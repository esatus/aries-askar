using aries_askar_dotnet.Models;
using aries_askar_dotnet.utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static aries_askar_dotnet.AriesAskar.NativeMethods;
using static indy_vdr_dotnet.models.Structures;

namespace aries_askar_dotnet.AriesAskar
{
    public static class StoreApi
    {
        #region Store calls
        public static async Task<string> GenerateRawKeyAsync(string seed)
        {
            return await StoreGenerateRawKeyAsync(seed);
        }

        public static async Task<Store> ProvisionAsync(
            string specUri,
            KeyMethod keyMethod = KeyMethod.NONE,
            string passKey = null,
            string profile = null,
            bool recreate = false)
        {
            return await StoreProvisionAsync(specUri, keyMethod, passKey, profile, recreate);
        }
        public static async Task<Store> OpenAsync(
            string specUri,
            KeyMethod keyMethod = KeyMethod.NONE,
            string passKey = null,
            string profile = null)
        {
            return await StoreOpenAsync(specUri, keyMethod, passKey, profile);
        }

        public static async Task<bool> RemoveAsync(this Store store, string specUri)
        {
            bool result = await StoreRemoveAsync(specUri);
            if (result)
            {
                if (store.session != null)
                {
                    store.session.sessionHandle = new IntPtr();
                    store.session.storeHandle = new IntPtr();
                    store.session = null;
                }
                store.storeHandle = new IntPtr();
            }
            return result;
        }

        public static async Task<string> CreateProfileAsync(this Store store, string profile = null)
        {
            return await StoreCreateProfileAsync(store.storeHandle, profile);
        }
        public static async Task<string> GetProfileNameAsync(this Store store)
        {
            return await StoreGetProfileNameAsync(store.storeHandle);
        }
        public static async Task<bool> RemoveProfileAsync(this Store store, string profile)
        {
            return await StoreRemoveProfileAsync(store.storeHandle, profile);
        }

        public static async Task<bool> RekeyAsync(
            this Store store,
            KeyMethod keyMethod = KeyMethod.NONE,
            string passKey = null)
        {
            return await StoreRekeyAsync(store.storeHandle, keyMethod, passKey);
        }

        public static async Task<bool> CloseAsync(this Store store, bool remove = false)
        {
            if (store.storeHandle != new IntPtr())
            {
                await StoreCloseAsync(store.storeHandle);
                if (store.session != null) 
                { 
                    store.session.sessionHandle = new IntPtr();
                    store.session.storeHandle = new IntPtr();
                    store.session = null;
                }
                store.storeHandle = new IntPtr();
            }
            if (remove)
            {
                return await store.RemoveAsync(store.specUri);
            }
            else return false;
        }

        public static async Task<Scan> StartScanAsync(
            this Store store,
            string category,
            string tagFilter = null,
            long offset = 0,
            long limit = -1, //None
            string profile = null)
        {
            return await StartScanAsync(store.storeHandle, category, tagFilter, offset, limit, profile);
        }

        public static async Task<Session> StartSessionAsync(this Store store, string profile = null, bool asTransactions = false)
        {
            if (store.storeHandle == new IntPtr())
            {
                throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot start session from closed store");
            }
            if (store.session != null)
            {
                if (store.session.sessionHandle != new IntPtr()) 
                    throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Session already opened");
            }
            Session session = await SessionStartAsync(store.storeHandle, profile, asTransactions);
            store.session = session;
            return session;
        }

        public static Session CreateSession(
            this Store store,
            string profile = null)
        {
            Session session = new Session(store.storeHandle, new IntPtr(), profile, false);
            store.session = session;
            return session;
        }

        public static Session CreateTransaction(
            this Store store,
            string profile = null)
        {
           
            Session session = new Session(store.storeHandle, new IntPtr(), profile, true);
            store.session = session;
            return session;
        }
        #endregion

        #region Scan calls

        public static async Task<IntPtr> NextAsync(this Scan scan)
        {
            return await ScanNextAsync(scan.scanHandle);
        }

        public static async Task<bool> FreeAsync(this Scan scan)
        {
            bool result = await ScanFreeAsync(scan.scanHandle);
            if (result)
            {
                scan.scanHandle = new IntPtr();
                scan.storeHandle = new IntPtr();
                scan.parameters = null;
            }
            return result;
        }
        #endregion

        #region Session calls
        public static async Task<Session> StartAsync(this Session session)
        {
            if (session.storeHandle == new IntPtr())
            {
                throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot start session from closed store");
            }
            if (session != null)
            {
                if (session.sessionHandle != new IntPtr())
                    throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Session already opened");
            }
            //when starting session from session object, we need to return our input session object because it is known to our store.
            //Otherwise we would return a new Session object but our store still has the old session without the right session handle.
            Session tempSess = await SessionStartAsync(session.storeHandle, session.sessionProfile, session.isTransaction);
            session.sessionHandle = tempSess.sessionHandle;
            return session;
        }
        public static async Task<long> CountAsync(this Session session, string category, string tagFilter = null)
        {
            if (session.sessionHandle == new IntPtr())
            {
                throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot count from closed session");
            }
            return await SessionCountAsync(session.sessionHandle, category, tagFilter);
        }

        //Return EntryListHandle
        public static async Task<IntPtr> FetchAsync(this Session session, string category, string name, bool forUpdate = false)
        {
            if (session.sessionHandle == new IntPtr())
            {
                throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot fetch from closed session");
            }
            return await SessionFetchAsync(session.sessionHandle, category, name);
        }

        //Return EntryListHandle
        public static async Task<IntPtr> FetchAllAsync(
            this Session session, 
            string category, 
            string tagFilter = null , 
            long limit = -1, //None
            bool forUpdate = false)
        {
            if (session.sessionHandle == new IntPtr())
            {
                throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot fetch from closed session");
            }
            return await SessionFetchAllAsync(session.sessionHandle, category, tagFilter, limit, forUpdate);
        }

        public static async Task<bool> InsertAsync(
            this Session session, 
            string category,
            string name,
            string value = null,
            string tags = null,
            long expiryMs = -1 //None
            )
        {
            if (session.sessionHandle == new IntPtr())
            {
                throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot update closed session");
            }
            return await SessionInsertAsync(session.sessionHandle, category, name, value, tags, expiryMs);
        }

        public static async Task<bool> ReplaceAsync(
            this Session session,
            string category,
            string name,
            string value = null,
            string tags = null,
            long expiryMs = -1 //None
            )
        {
            if (session.sessionHandle == new IntPtr())
            {
                throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot update closed session");
            }
            return await SessionReplaceAsync(session.sessionHandle, category, name, value, tags, expiryMs);
        }

        public static async Task<bool> RemoveAsync(
            this Session session,
            string category,
            string name)
        {
            if (session.sessionHandle == new IntPtr())
            {
                throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot remove for closed session");
            }
            return await SessionRemoveAsync(session.sessionHandle, category, name);
        }

        //Return number of removed elements
        public static async Task<long> RemoveAllAsync(this Session session,string category, string tagFilter = null)
        {
            if (session.sessionHandle == new IntPtr())
            {
                throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot remove all for closed session");
            }
            return await SessionRemoveAllAsync(session.sessionHandle, category, tagFilter);
        }

        public static async Task<bool> InsertKeyAsync(
            this Session session,
            IntPtr localKeyHandle,
            string name,
            string metaData = null,
            string tags = null,
            long expiryMs = -1 //None
            )
        {
            if (session.sessionHandle == new IntPtr())
            {
                throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot insert key with closed session");
            }
            return await SessionInsertKeyAsync(session.sessionHandle, localKeyHandle, name, metaData, tags, expiryMs);
        }

        //Returns KeyEntryListHandle
        public static async Task<IntPtr> FetchKeyAsync(this Session session, string name, bool forUpdate = false)
        {
            if (session.sessionHandle == new IntPtr())
            {
                throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot fetch key from closed session");
            }
            return await SessionFetchKeyAsync(session.sessionHandle, name, forUpdate);
        }

        //Returns KeyEntryListHandle
        public static async Task<IntPtr> FetchAllKeysAsync(
            this Session session,
            KeyAlg keyAlg = KeyAlg.NONE,
            string thumbprint = null,
            string tagFilter = null,
            long limit = -1, //None
            bool forUpdate = false)
        {
            if (session.sessionHandle == new IntPtr())
            {
                throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot fetch key from closed session");
            }
            return await SessionFetchAllKeysAsync(session.sessionHandle, keyAlg, thumbprint, tagFilter, limit, forUpdate);
        }

        public static async Task<bool> UpdateKeyAsync(
            this Session session,
            string name,
            string metaData = null,
            string tags = null,
            long expiryMs = -1 //None
            )
        {
            if (session.sessionHandle == new IntPtr())
            {
                throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot update key with closed session");
            }
            return await SessionUpdateKeyAsync(session.sessionHandle, name, metaData, tags, expiryMs);
        }

        public static async Task<bool> RemoveKeyAsync(
            this Session session,
            string name)
        {
            if (session.sessionHandle == new IntPtr())
            {
                throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot remove key with closed session");
            }
            return await SessionRemoveKeyAsync(session.sessionHandle, name);
        }

        public static async Task<bool> CloseAndCommitAsync(
            this Session session)
        {
            if (session != null)
            {
                if (session.sessionHandle == new IntPtr())
                {
                    throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot close and commit already closed sessions or transactions");
                }
                bool res = await SessionCloseAndCommitAsync(session.sessionHandle);
                session.sessionHandle = new IntPtr();
                return res;
            }
            else throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Session is null");
        }

        public static async Task<bool> CloseAndRollbackAsync(
            this Session session)
        {
            if (session != null)
            {
                if (session.sessionHandle == new IntPtr())
                {
                    throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot close and rollback already closed sessions or transactions");
                }
                bool res = await SessionCloseAndRollbackAsync(session.sessionHandle);
                session.sessionHandle = new IntPtr();
                return res;
            }
            else throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Session is null");
        }
        #endregion

        #region Store
        public static async Task<string> StoreGenerateRawKeyAsync(string seed)
        {
            string result = "";
            int errorCode = NativeMethods.askar_store_generate_raw_key(ByteBuffer.Create(seed), ref result);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return result;
        }

        private static async Task<Store> StoreProvisionAsync(
            string specUri, 
            KeyMethod keyMethod = KeyMethod.NONE, 
            string passKey = null, 
            string profile = null, 
            bool recreate = false)
        {
            var taskCompletionSource = new TaskCompletionSource<IntPtr>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_store_provision(
                FfiStr.Create(specUri),
                FfiStr.Create(keyMethod.ToKeyMethodString()),
                FfiStr.Create(passKey),
                FfiStr.Create(profile),
                Convert.ToByte(recreate),
                GetStoreHandleCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return new Store(await taskCompletionSource.Task, specUri);
        }

        private static async Task<Store> StoreOpenAsync(
            string specUri, 
            KeyMethod keyMethod = KeyMethod.NONE, 
            string passKey = null, 
            string profile = null)
        {
            var taskCompletionSource = new TaskCompletionSource<IntPtr>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_store_open(
                FfiStr.Create(specUri),
                FfiStr.Create(keyMethod.ToKeyMethodString()),
                FfiStr.Create(passKey),
                FfiStr.Create(profile),
                GetStoreHandleCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return new Store(await taskCompletionSource.Task, specUri);
        }

        private static async Task<bool> StoreRemoveAsync(string specUri)
        {
            var taskCompletionSource = new TaskCompletionSource<byte>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_store_remove(
                FfiStr.Create(specUri),
                GetStoreByteCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return Convert.ToBoolean(await taskCompletionSource.Task);
        }

        private static async Task<string> StoreCreateProfileAsync(IntPtr storeHandle, string profile = null)
        {
            var taskCompletionSource = new TaskCompletionSource<string>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_store_create_profile(
                storeHandle,
                FfiStr.Create(profile),
                GetStoreStringCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }

        private static async Task<string> StoreGetProfileNameAsync(IntPtr storeHandle)
        {
            var taskCompletionSource = new TaskCompletionSource<string>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_store_get_profile_name(
                storeHandle,
                GetStoreStringCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }

        private static async Task<bool> StoreRemoveProfileAsync(IntPtr storeHandle, string profile)
        {
            var taskCompletionSource = new TaskCompletionSource<byte>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_store_remove_profile(
                storeHandle,
                FfiStr.Create(profile),
                GetStoreByteCallback,
                callbackId); ;

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return Convert.ToBoolean(await taskCompletionSource.Task);
        }

        private static async Task<bool> StoreRekeyAsync(
            IntPtr storeHandle,
            KeyMethod keyMethod = KeyMethod.NONE,
            string passKey = null)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_store_rekey(
                storeHandle,
                FfiStr.Create(keyMethod.ToKeyMethodString()),
                FfiStr.Create(passKey),
                NoReturnValueStoreCallback,
                callbackId); ;

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }

        private static async Task<bool> StoreCloseAsync(IntPtr storeHandle)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_store_close(
                storeHandle,
                NoReturnValueStoreCallback,
                callbackId); ;

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }
        #endregion

        #region Scan
        //Returns a scanHandle
        private static async Task<Scan> StartScanAsync(
            IntPtr storeHandle, 
            string category, 
            string tagFilter = null, 
            long offset = 0, 
            long limit = -1, //None 
            string profile = null)
        {
            var taskCompletionSource = new TaskCompletionSource<IntPtr>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_scan_start(
                storeHandle,
                FfiStr.Create(profile),
                FfiStr.Create(category),
                FfiStr.Create(tagFilter),
                offset,
                limit,
                GetStoreHandleCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            List<object> parameters = new() {profile, category, tagFilter, offset, limit};
            return new Scan(await taskCompletionSource.Task, storeHandle, parameters);
        }

        //Returns an entryListHandle
        private static async Task<IntPtr> ScanNextAsync(IntPtr scanHandle)
        {
            var taskCompletionSource = new TaskCompletionSource<IntPtr>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_scan_next(
                scanHandle,
                GetStoreHandleCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }

        private static async Task<bool> ScanFreeAsync(IntPtr scanHandle)
        {
            int errorCode = NativeMethods.askar_scan_free(scanHandle);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            else
                return true;   
        }
        #endregion

        #region Session
        //Returns a sessionHandle
        private static async Task<Session> SessionStartAsync(IntPtr storeHandle, string profile = null, bool asTransactions = false)
        {
            var taskCompletionSource = new TaskCompletionSource<IntPtr>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_session_start(
                storeHandle,
                FfiStr.Create(profile),
                Convert.ToByte(asTransactions),
                GetStoreHandleCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return new Session(storeHandle, await taskCompletionSource.Task, profile, asTransactions);
        }

        private static async Task<long> SessionCountAsync(IntPtr sessionHandle, string category, string tagFilter = null)
        {
            var taskCompletionSource = new TaskCompletionSource<long>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_session_count(
                sessionHandle,
                FfiStr.Create(category),
                FfiStr.Create(tagFilter),
                GetStoreLongCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }

        //Returns an EntryListHandle
        private static async Task<IntPtr> SessionFetchAsync(IntPtr sessionHandle, string category, string name, bool forUpdate = false)
        {
            var taskCompletionSource = new TaskCompletionSource<IntPtr>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_session_fetch(
                sessionHandle,
                FfiStr.Create(category),
                FfiStr.Create(name),
                Convert.ToByte(forUpdate),
                GetStoreHandleCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return await taskCompletionSource.Task;
            //return new EntryList(await taskCompletionSource.Task);
        }

        //Returns an EntryListHandle
        private static async Task<IntPtr> SessionFetchAllAsync(
            IntPtr sessionHandle, 
            string category, 
            string tagFilter = null, 
            long limit = -1, //None 
            bool forUpdate = false)
        {
            var taskCompletionSource = new TaskCompletionSource<IntPtr>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_session_fetch_all(
                sessionHandle,
                FfiStr.Create(category),
                FfiStr.Create(tagFilter),
                limit,
                Convert.ToByte(forUpdate),
                GetStoreHandleCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }

        private static async Task<long> SessionRemoveAllAsync(IntPtr sessionHandle, string category, string tagFilter = null)
        {
            var taskCompletionSource = new TaskCompletionSource<long>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_session_remove_all(
                sessionHandle,
                FfiStr.Create(category),
                FfiStr.Create(tagFilter),
                GetStoreLongCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }
        private static async Task<bool> SessionInsertAsync(
            IntPtr sessionHandle,
            string category,
            string name,
            string value = null,
            string tags = null,
            long expiryMs = -1 //None
            )
        {
            bool res = await SessionUpdateAsync(
                sessionHandle,
                UpdateOperation.Insert,
                category,
                name,
                value,
                tags,
                expiryMs);

            return await Task.FromResult(res);
        }

        private static async Task<bool> SessionReplaceAsync(
            IntPtr sessionHandle,
            string category,
            string name,
            string value = null,
            string tags = null,
            long expiryMs = -1 //None
            )
        {
            bool res = await SessionUpdateAsync(
                sessionHandle,
                UpdateOperation.Replace,
                category,
                name,
                value,
                tags,
                expiryMs);

            return await Task.FromResult(res);
        }

        private static async Task<bool> SessionRemoveAsync(
            IntPtr sessionHandle,
            string category,
            string name)
        {
            bool res = await SessionUpdateAsync(
                sessionHandle,
                UpdateOperation.Remove,
                category,
                name);

            return await Task.FromResult(res);
        }

        private static async Task<bool> SessionUpdateAsync(
            IntPtr sessionHandle, 
            UpdateOperation operation, 
            string category, 
            string name, 
            string value = null, 
            string tags = null, 
            long expiryMs = -1 //None
            )
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_session_update(
                sessionHandle,
                Convert.ToByte(operation),
                FfiStr.Create(category),
                FfiStr.Create(name),
                ByteBuffer.Create(value),
                FfiStr.Create(tags),
                expiryMs,
                NoReturnValueStoreCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }

        private static async Task<bool> SessionInsertKeyAsync(
            IntPtr sessionHandle, 
            IntPtr localKeyHandle, 
            string name, 
            string metaData = null, 
            string tags = null,
            long expiryMs = -1 //None
            )
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_session_insert_key(
                sessionHandle,
                localKeyHandle,
                FfiStr.Create(name),
                FfiStr.Create(metaData),
                FfiStr.Create(tags),
                expiryMs,
                NoReturnValueStoreCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }

        //Returns a keyEntryListHandle
        private static async Task<IntPtr> SessionFetchKeyAsync(IntPtr sessionHandle, string name, bool forUpdate = false)
        {
            var taskCompletionSource = new TaskCompletionSource<IntPtr>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_session_fetch_key(
                sessionHandle,
                FfiStr.Create(name),
                Convert.ToByte(forUpdate),
                GetStoreHandleCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }

        //Returns a keyEntryListHandle
        private static async Task<IntPtr> SessionFetchAllKeysAsync(
            IntPtr sessionHandle, 
            KeyAlg keyAlg = KeyAlg.NONE, 
            string thumbprint = null, 
            string tagFilter = null, 
            long limit = -1, //None 
            bool forUpdate = false)
        {
            var taskCompletionSource = new TaskCompletionSource<IntPtr>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_session_fetch_all_keys(
                sessionHandle,
                FfiStr.Create(keyAlg.ToKeyAlgString()),
                FfiStr.Create(thumbprint),
                FfiStr.Create(tagFilter),
                limit,
                Convert.ToByte(forUpdate),
                GetStoreHandleCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }

        private static async Task<bool> SessionUpdateKeyAsync(
            IntPtr sessionHandle, 
            string name, 
            string metaData = null, 
            string tags = null, 
            long expiryMs = -1 //None
            )
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_session_update_key(
                sessionHandle,
                FfiStr.Create(name),
                FfiStr.Create(metaData),
                FfiStr.Create(tags),
                expiryMs,
                NoReturnValueStoreCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }
        private static async Task<bool> SessionRemoveKeyAsync(IntPtr sessionHandle, string name)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_session_remove_key(
                sessionHandle,
                FfiStr.Create(name),
                NoReturnValueStoreCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }

        private static async Task<bool> SessionCloseAndCommitAsync(IntPtr sessionHandle)
        {
            bool res = await SessionCloseAsync(
                sessionHandle,
                true);

            return await Task.FromResult(res);
        }

        private static async Task<bool> SessionCloseAndRollbackAsync(IntPtr sessionHandle)
        {
            bool res = await SessionCloseAsync(
                sessionHandle,
                false);

            return await Task.FromResult(res);
        }

        private static async Task<bool> SessionCloseAsync(IntPtr sessionHandle, bool commit)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_session_close(
                sessionHandle,
                Convert.ToByte(commit),
                NoReturnValueStoreCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }
        #endregion

        #region Callback methods
        private static void NoReturnValueStoreCallbackMethod(long callback_id, int err)
        {
            var taskCompletionSource = PendingCallbacks.Remove<bool>(callback_id);

            if (err != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                taskCompletionSource.SetException(AriesAskarException.FromSdkError(error));
                return;
            }
            taskCompletionSource.SetResult(true);
        }
        private static NoReturnValueStoreCompletedDelegate NoReturnValueStoreCallback = NoReturnValueStoreCallbackMethod;

        private static void GetStoreHandleCallbackMethod(long callback_id, int err, IntPtr handle)
        {
            var taskCompletionSource = PendingCallbacks.Remove<IntPtr>(callback_id);

            if (err != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                taskCompletionSource.SetException(AriesAskarException.FromSdkError(error));
                return;
            }
            taskCompletionSource.SetResult(handle);
        }
        private static GetStoreHandleCompletedDelegate GetStoreHandleCallback = GetStoreHandleCallbackMethod;

        private static void GetStoreByteCallbackMethod(long callback_id, int err, byte result)
        {
            var taskCompletionSource = PendingCallbacks.Remove<byte>(callback_id);

            if (err != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                taskCompletionSource.SetException(AriesAskarException.FromSdkError(error));
                return;
            }
            taskCompletionSource.SetResult(result);
        }
        private static GetStoreByteCompletedDelegate GetStoreByteCallback = GetStoreByteCallbackMethod;

        private static void GetStoreLongCallbackMethod(long callback_id, int err, long result)
        {
            var taskCompletionSource = PendingCallbacks.Remove<long>(callback_id);

            if (err != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                taskCompletionSource.SetException(AriesAskarException.FromSdkError(error));
                return;
            }
            taskCompletionSource.SetResult(result);
        }
        private static GetStoreLongCompletedDelegate GetStoreLongCallback = GetStoreLongCallbackMethod;

        private static void GetStoreStringCallbackMethod(long callback_id, int err, string result)
        {
            var taskCompletionSource = PendingCallbacks.Remove<string>(callback_id);

            if (err != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                taskCompletionSource.SetException(AriesAskarException.FromSdkError(error));
                return;
            }
            taskCompletionSource.SetResult(result);
        }
        private static GetStoreStringCompletedDelegate GetStoreStringCallback = GetStoreStringCallbackMethod;
        #endregion
    }
}
