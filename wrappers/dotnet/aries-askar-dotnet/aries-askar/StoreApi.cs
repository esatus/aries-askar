using aries_askar_dotnet.Models;
using aries_askar_dotnet.utils;
using System;
using System.Threading.Tasks;
using static aries_askar_dotnet.aries_askar.NativeMethods;
using static indy_vdr_dotnet.models.Structures;

namespace aries_askar_dotnet.aries_askar
{
    public class StoreApi
    {
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

        public static async Task<uint> StoreProvisionAsync(string specUri, string keyMethod, string passKey, string profile, bool recreate)
        {
            var taskCompletionSource = new TaskCompletionSource<uint>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_store_provision(
                FfiStr.Create(specUri),
                FfiStr.Create(keyMethod),
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

            return await taskCompletionSource.Task;
        }

        public static async Task<uint> StoreOpenAsync(string specUri, string keyMethod, string passKey, string profile)
        {
            var taskCompletionSource = new TaskCompletionSource<uint>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_store_open(
                FfiStr.Create(specUri),
                FfiStr.Create(keyMethod),
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

            return await taskCompletionSource.Task;
        }

        public static async Task<bool> StoreRemoveAsync(string specUri)
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

        public static async Task<string> StoreCreateProfileAsync(uint storeHandle, string profile)
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

        public static async Task<string> StoreGetProfileNameAsync(uint storeHandle)
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

        public static async Task<bool> StoreRemoveProfileAsync(uint storeHandle, string profile)
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

        public static async Task<bool> StoreRekeyAsync(uint storeHandle, string keyMethod, string passKey)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_store_rekey(
                storeHandle,
                FfiStr.Create(keyMethod),
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

        public static async Task<bool> StoreCloseAsync(uint storeHandle)
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
        public static async Task<uint> ScanStartAsync(uint storeHandle, string profile, string category, string tagFilter, long offset, long limit)
        {
            var taskCompletionSource = new TaskCompletionSource<uint>();
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

            return await taskCompletionSource.Task;
        }

        //Returns an entryListHandle
        public static async Task<uint> ScanNextAsync(uint scanHandle)
        {
            var taskCompletionSource = new TaskCompletionSource<uint>();
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

        public static async Task<bool> ScanFreeAsync(uint scanHandle)
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
        public static async Task<uint> SessionStartAsync(uint storeHandle, string profile, bool asTransactions)
        {
            var taskCompletionSource = new TaskCompletionSource<uint>();
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

            return await taskCompletionSource.Task;
        }

        public static async Task<long> SessionCountAsync(uint storeHandle, string category, string tagFilter)
        {
            var taskCompletionSource = new TaskCompletionSource<long>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_session_count(
                storeHandle,
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
        public static async Task<uint> SessionFetchAsync(uint sessionHandle, string category, string name, bool forUpdate)
        {
            var taskCompletionSource = new TaskCompletionSource<uint>();
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
        }

        //Returns an EntryListHandle
        public static async Task<uint> SessionFetchAllAsync(uint sessionHandle, string category, string tagFilter, long limit, bool forUpdate)
        {
            var taskCompletionSource = new TaskCompletionSource<uint>();
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

        public static async Task<long> SessionRemoveAllAsync(uint sessionHandle, string category, string tagFilter)
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

        public static async Task<bool> SessionUpdateAsync(uint sessionHandle, UpdateOperation operation, string category, string name, string value, string tags, long expiryMs)
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

        public static async Task<bool> SessionInsertKeyAsync(uint sessionHandle, uint localKeyHandle, string name, string metaData, string tags, long expiryMs)
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
        public static async Task<uint> SessionFetchKeyAsync(uint sessionHandle, string name, bool forUpdate)
        {
            var taskCompletionSource = new TaskCompletionSource<uint>();
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
        public static async Task<uint> SessionFetchAllKeysAsync(uint sessionHandle, KeyAlg keyAlg, string thumbprint, string tagFilter, long limit, bool forUpdate)
        {
            var taskCompletionSource = new TaskCompletionSource<uint>();
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

        public static async Task<bool> SessionUpdateKeyAsync(uint sessionHandle, string name, string metaData, string tags, long expiryMs)
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
        public static async Task<bool> SessionRemoveKeyAsync(uint sessionHandle, string name)
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

        public static async Task<bool> SessionCloseAsync(uint sessionHandle, bool commit)
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

        private static void GetStoreHandleCallbackMethod(long callback_id, int err, uint handle)
        {
            var taskCompletionSource = PendingCallbacks.Remove<uint>(callback_id);

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
