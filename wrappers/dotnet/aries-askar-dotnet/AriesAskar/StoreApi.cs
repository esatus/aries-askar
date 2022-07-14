using aries_askar_dotnet.Models;
using aries_askar_dotnet.utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static aries_askar_dotnet.AriesAskar.NativeMethods;
using static aries_askar_dotnet.Models.Structures;

namespace aries_askar_dotnet.AriesAskar
{
    public static class StoreApi
    {
        #region Store calls
        /// <summary>
        /// Generates and returns a new raw store key (non-derived) from a given seed.
        /// </summary>
        /// <param name="seed">A seed phrase with a length of 32.</param>
        /// <returns>A random raw store key (non-derived) in <see cref="string"/> format, created from a given seed.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<string> GenerateRawKeyAsync(string seed)
        {
            return await StoreGenerateRawKeyAsync(seed);
        }

        /// <summary>
        /// Provision a new <see cref="Store"/> in the backend.
        /// </summary>
        /// <param name="specUri">The provison spec uri <see cref="string"/>.</param>
        /// <param name="keyMethod">The key method from enum <see cref="KeyMethod"/>, which represents supported methods for generating or referencing a new store key; default none.</param>
        /// <param name="passKey">The pass key <see cref="string"/>, a possible empty password or key used to derive a store key; default null.</param>
        /// <param name="profile">The store profile <see cref="string"/>; default null.</param>
        /// <param name="recreate">The recreate <see cref="bool"/> flag; default false.</param>
        /// <returns>A new instance of <see cref="Store"/> containing the spec uri and store handle from the backend.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<Store> ProvisionAsync(
            string specUri,
            KeyMethod keyMethod = KeyMethod.NONE,
            string passKey = null,
            string profile = null,
            bool recreate = false)
        {
            return await StoreProvisionAsync(specUri, keyMethod, passKey, profile, recreate);
        }

        /// <summary>
        /// Open an existing <see cref="Store"/> from the backend.
        /// </summary>
        /// <param name="specUri">The spec uri <see cref="string"/> of the store.</param>
        /// <param name="keyMethod">The key method from enum <see cref="KeyMethod"/>, which represents supported methods for generating or referencing a new store key; default none.</param>
        /// <param name="passKey">The pass key as <see cref="string"/>, a possible empty password or key used to derive a store key; default null.</param>
        /// <param name="profile">The store profile name as <see cref="string"/>; default null.</param>
        /// <returns>The store instance of the existing <see cref="Store"/> containing the spec uri and store handle from the backend.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<Store> OpenAsync(
            string specUri,
            KeyMethod keyMethod = KeyMethod.NONE,
            string passKey = null,
            string profile = null)
        {
            return await StoreOpenAsync(specUri, keyMethod, passKey, profile);
        }

        /// <summary>
        /// Remove an existing <see cref="Store"/> from the backend and reset the store object.
        /// </summary>
        /// <param name="specUri">The spec uri <see cref="string"/> of the store to be removed.</param>
        /// <param name="store">The <see cref="Store"/> instance, holding the store handle from the backend.</param>
        /// <returns>The result of the remove call as <see cref="bool"/>; true if succeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
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

        /// <summary>
        /// Create a new profile with the given profile name in the backend. A random name is used if profile is null.
        /// </summary>
        /// <param name="profile">The profile name as <see cref="string"/>; default null.</param>
        /// <param name="store">The <see cref="Store"/> instance where profile is created, holding the store handle from the backend.</param>
        /// <returns>The profile name of the created profile as <see cref="string"/>; if profile name is null, a random name is used by the backend.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<string> CreateProfileAsync(this Store store, string profile = null)
        {
            return await StoreCreateProfileAsync(store.storeHandle, profile);
        }

        /// <summary>
        /// Get the default profile name used when starting a scan or a session from the backend.
        /// </summary>
        /// <param name="store">The <see cref="Store"/> instance, holding the store handle from the backend.</param>
        /// <returns>The default profile name as <see cref="string"/></returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<string> GetProfileNameAsync(this Store store)
        {
            return await StoreGetProfileNameAsync(store.storeHandle);
        }

        /// <summary>
        /// Remove an existing profile from the backend with the given profile name.
        /// </summary>
        /// <param name="profile">The profile name of the profile to remove as <see cref="string"/>.</param>
        /// <param name="store">The <see cref="Store"/> instance where a profile is to be removed, holding the store handle from the backend.</param>
        /// <returns>The result of the remove call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<bool> RemoveProfileAsync(this Store store, string profile)
        {
            return await StoreRemoveProfileAsync(store.storeHandle, profile);
        }

        /// <summary>
        /// Replace the wrapping key on a <see cref="Store"/> in the backend.
        /// </summary>
        /// <param name="store">The <see cref="Store"/> instance where to rekey, holding the store handle from the backend.</param>
        /// <param name="keyMethod">The key method from enum <see cref="KeyMethod"/>, which represents supported methods for generating or referencing a new store key; default none.</param>
        /// <param name="passKey">The pass key as <see cref="string"/>, a possible empty password or key used to derive a store key; default null.</param>
        /// <returns>The result of the rekey call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<bool> RekeyAsync(
            this Store store,
            KeyMethod keyMethod = KeyMethod.NONE,
            string passKey = null)
        {
            return await StoreRekeyAsync(store.storeHandle, keyMethod, passKey);
        }

        /// <summary>
        /// Close the open <see cref="Store"/> in the backend with the possibility to also remove it and reset the store object.
        /// </summary>
        /// <param name="store">The <see cref="Store"/> instance to close, holding the store handle from the backend.</param>
        /// <param name="remove">A <see cref="bool"/> flag wether to just close (false) or also remove (true) the store; default false.</param>
        /// <returns>The decision of the close call as <see cref="bool"/>; false if just close, true if also removed.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<bool> CloseAsync(this Store store, bool remove = false)
        {
            if (store.storeHandle != new IntPtr())
            {
                _ = await StoreCloseAsync(store.storeHandle);
                if (store.session != null)
                {
                    store.session.sessionHandle = new IntPtr();
                    store.session.storeHandle = new IntPtr();
                    store.session = null;
                }
                store.storeHandle = new IntPtr();
            }
            return remove && await store.RemoveAsync(store.specUri);
        }

        /// <summary>
        /// Create a new <see cref="Scan"/> instance against the store in the backend with the given scan parameters. The result will keep an open connection to the backend until it is consumed.
        /// </summary>
        /// <param name="store">The <see cref="Store"/> instance to scan, holding the store handle from the backend.</param>
        /// <param name="category">The category filter parameter as <see cref="string"/>.</param>
        /// <param name="tagFilter">The tag filter parameter as <see cref="string"/>; default null.</param>
        /// <param name="offset">A offset parameter as <see cref="long"/> to offset a number of the total results; default 0.</param>
        /// <param name="limit">A limit parameter as <see cref="long"/> to limit the number of the total received results; default -1 which indicates no limit.</param>
        /// <param name="profile">The profile name filter parameter as <see cref="string"/>; default null.</param>
        /// <returns>A new <see cref="Scan"/> instance containing the scan handle, store handle of the backend and the given parameters.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
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

        /// <summary>
        /// Create a new <see cref="Session"/> instance against the store in the backend with a given profile name as session or transaction. Also adds the session to the store object.
        /// </summary>
        /// <param name="store">The <see cref="Store"/> instance, holding the store handle from the backend.</param>
        /// <param name="profile">The profile name of the session / transaction as <see cref="string"/>. A random name is used by the backend when null; default null.</param>
        /// <param name="asTransactions">The <see cref="bool"/> flag, which indicates a session (false) or transaction (true) ; default false.</param>
        /// <returns>A new <see cref="Session"/> instance containing the store handle, session handle of the backend and the asTransaction and profile parameters.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<Session> StartSessionAsync(this Store store, string profile = null, bool asTransactions = false)
        {
            if (store.storeHandle == new IntPtr())
            {
                throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot start session from closed store");
            }
            if (store.session != null)
            {
                if (store.session.sessionHandle != new IntPtr())
                {
                    throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Session already opened");
                }
            }
            Session session = await SessionStartAsync(store.storeHandle, profile, asTransactions);
            store.session = session;
            return session;
        }

        /// <summary>
        /// Create a new <see cref="Session"/> instance as session with a given profile name and adds the <see cref="Session"/> to the store object. The <see cref="Session"/> is not yet started in the backend.
        /// </summary>
        /// <param name="store">The <see cref="Store"/> instance, holding the store handle from the backend.</param>
        /// <param name="profile">The profile name of the session as <see cref="string"/>; default null.</param>
        /// <returns>A new <see cref="Session"/> instance containing the store handle of the backend and the asTransaction and profile parameters.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static Session CreateSession(
            this Store store,
            string profile = null)
        {
            Session session = new Session(store.storeHandle, new IntPtr(), profile, false);
            store.session = session;
            return session;
        }

        /// <summary>
        /// Create a new <see cref="Session"/> instance as transaction with a given profile name and adds the <see cref="Session"/> to the store object. The <see cref="Session"/> is not yet started in the backend.
        /// </summary>
        /// <param name="store">The <see cref="Store"/> instance, holding the store handle from the backend</param>
        /// <param name="profile">The profile name of the transaction as <see cref="string"/>; default null.</param>
        /// <returns>A new <see cref="Session"/> instance containing the store handle of the backend and the asTransaction and profile parameters.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
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

        /// <summary>
        /// Fetch the results for the given <see cref="Scan"/> object as an entryListHandle from the backend.
        /// </summary>
        /// <param name="scan">The <see cref="Scan"/> instance, holding the scan handle from the backend.</param>
        /// <returns>An entryListHandle as <see cref="IntPtr"/> which can be used as input for the methods in <see cref="ResultListApi"/> to obtain parameters from the records in the store.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<IntPtr> NextAsync(this Scan scan)
        {
            return await ScanNextAsync(scan.scanHandle);
        }

        /// <summary>
        /// Free the scan object in the backend and reset the <see cref="Scan"/> object.
        /// </summary>
        /// <param name="scan">The <see cref="Scan"/> instance, holding the scan handle from the backend.</param>
        /// <returns>The result of the free call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
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

        /// <summary>
        /// Start an already created session/transaction from a given <see cref="Session"/> instance in the backend. Also adds the session to the store object.
        /// </summary>
        /// <param name="session">The <see cref="Session"/> instance, holding the store and session handle from the backend.</param>
        /// <returns>A new <see cref="Session"/> instance containing the store handle, session handle of the backend and the asTransaction and profile parameters.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<Session> StartAsync(this Session session)
        {
            if (session.storeHandle == new IntPtr())
            {
                throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot start session from closed store");
            }
            if (session != null)
            {
                if (session.sessionHandle != new IntPtr())
                {
                    throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Session already opened");
                }
            }
            //when starting session from session object, we need to return our input session object because it is known to our store.
            //Otherwise we would return a new Session object but our store still has the old session without the right session handle.
            Session tempSess = await SessionStartAsync(session.storeHandle, session.sessionProfile, session.isTransaction);
            session.sessionHandle = tempSess.sessionHandle;
            return session;
        }

        /// <summary>
        /// Count the number of entries for a given record category.
        /// </summary>
        /// <param name="session">The <see cref="Session"/> instance, holding the store and session handle from the backend.</param>
        /// <param name="category">The category parameter as <see cref="string"/>.</param>
        /// <param name="tagFilter">The tag filter parameter as <see cref="string"/>; default null.</param>
        /// <returns>The number of entries as <see cref="long"/> matching the given category and tag filter.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<long> CountAsync(this Session session, string category, string tagFilter = null)
        {
            return session.sessionHandle == new IntPtr()
                ? throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot count from closed session")
                : await SessionCountAsync(session.sessionHandle, category, tagFilter);
        }

        /// <summary>
        /// Fetch the current record matching a given category and name parameter from the <see cref="Session"/> object from the backend as an entryListHandle.
        /// </summary>
        /// <param name="session">The <see cref="Session"/> instance, holding the store and session handle from the backend.</param>
        /// <param name="category">The category parameter as <see cref="string"/>.</param>
        /// <param name="name">The name parameter as <see cref="string"/>.</param>
        /// <param name="forUpdate">The <see cref="bool"/> flag. Specify `for_update` when in a transaction to create an update lock on the
        /// associated record, if supported by the store backend; default false.</param>
        /// <returns>A entryList handle as <see cref="IntPtr"/> from the backend.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<IntPtr> FetchAsync(this Session session, string category, string name, bool forUpdate = false)
        {
            return session.sessionHandle == new IntPtr()
                ? throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot fetch from closed session")
                : await SessionFetchAsync(session.sessionHandle, category, name);
        }

        /// <summary>
        /// Fetch all records matching the given category and tag filter parameters from the <see cref="Session"/> object from the backend as an entryListHandle. This method may be used within a transaction. It should
        /// not be used for very large result sets due to correspondingly large memory requirements.
        /// </summary>
        /// <param name="session">The <see cref="Session"/> instance, holding the store and session handle from the backend.</param>
        /// <param name="category">The category parameter as <see cref="string"/>.</param>
        /// <param name="tagFilter">The tag filter parameter as <see cref="string"/>.</param>
        /// <param name="limit">A limit parameter as <see cref="long"/> to limit the number of the total received results; default -1 which indicates no limit.</param>
        /// <param name="forUpdate">The <see cref="bool"/> flag. Specify `for_update` when in a transaction to create an update lock on the
        /// associated record, if supported by the store backend; default false.</param>
        /// <returns>A entryList handle as <see cref="IntPtr"/> from the backend.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<IntPtr> FetchAllAsync(
            this Session session,
            string category,
            string tagFilter = null,
            long limit = -1, //None
            bool forUpdate = false)
        {
            return session.sessionHandle == new IntPtr()
                ? throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot fetch from closed session")
                : await SessionFetchAllAsync(session.sessionHandle, category, tagFilter, limit, forUpdate);
        }

        /// <summary>
        /// Insert a new record into the <see cref="Store"/> in the backend.
        /// </summary>
        /// <param name="session">The <see cref="Session"/> instance, holding the store and session handle from the backend.</param>
        /// <param name="category">The category parameter as <see cref="string"/>.</param>
        /// <param name="name">The name parameter as <see cref="string"/>.</param>
        /// <param name="value">The value parameter as <see cref="string"/>; default null.</param>
        /// <param name="tags">The tag parameter as <see cref="string"/>; default null.</param>
        /// <param name="expiryMs">The expiry in ms as <see cref="long"/>; default -1 which indicates no expiry.</param>
        /// <returns>The result of the insert call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<bool> InsertAsync(
            this Session session,
            string category,
            string name,
            string value = null,
            string tags = null,
            long expiryMs = -1 //None
            )
        {
            return session.sessionHandle == new IntPtr()
                ? throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot update closed session")
                : await SessionInsertAsync(session.sessionHandle, category, name, value, tags, expiryMs);
        }

        /// <summary>
        /// Replace the value and tags of a record in the <see cref="Store"/> in the backend.
        /// </summary>
        /// <param name="session">The <see cref="Session"/> instance, holding the store and session handle from the backend.</param>
        /// <param name="category">The category parameter as <see cref="string"/>.</param>
        /// <param name="name">The name parameter as <see cref="string"/>.</param>
        /// <param name="value">The value parameter as <see cref="string"/>; default null.</param>
        /// <param name="tags">The tag parameter as <see cref="string"/>; default null.</param>
        /// <param name="expiryMs">The expiry in ms as <see cref="long"/>; default -1 which indicates no expiry.</param>
        /// <returns>The result of the replace call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<bool> ReplaceAsync(
            this Session session,
            string category,
            string name,
            string value = null,
            string tags = null,
            long expiryMs = -1 //None
            )
        {
            return session.sessionHandle == new IntPtr()
                ? throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot update closed session")
                : await SessionReplaceAsync(session.sessionHandle, category, name, value, tags, expiryMs);
        }

        /// <summary>
        /// Remove a record from the <see cref="Store"/> in the backend matching the given category and name.
        /// </summary>
        /// <param name="session">The <see cref="Session"/> instance, holding the store and session handle from the backend.</param>
        /// <param name="category">The category parameter as <see cref="string"/>.</param>
        /// <param name="name">The name parameter as <see cref="string"/>.</param>
        /// <returns>The result of the remove call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<bool> RemoveAsync(
            this Session session,
            string category,
            string name)
        {
            return session.sessionHandle == new IntPtr()
                ? throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot remove for closed session")
                : await SessionRemoveAsync(session.sessionHandle, category, name);
        }

        /// <summary>
        /// Remove all records from the <see cref="Store"/> in the backend matching the given category and tag filter.
        /// </summary>
        /// <param name="session">The <see cref="Session"/> instance, holding the store and session handle from the backend.</param>
        /// <param name="category">The category parameter as <see cref="string"/>.</param>
        /// <param name="tagFilter">The tag filter parameter as <see cref="string"/>; default null.</param>
        /// <returns>The number of removed records as <see cref="long"/>.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<long> RemoveAllAsync(this Session session, string category, string tagFilter = null)
        {
            return session.sessionHandle == new IntPtr()
                ? throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot remove all for closed session")
                : await SessionRemoveAllAsync(session.sessionHandle, category, tagFilter);
        }

        /// <summary>
        /// Insert a local key instance into the <see cref="Store"/> in the backend.
        /// </summary>
        /// <param name="session">The <see cref="Session"/> instance, holding the store and session handle from the backend.</param>
        /// <param name="localKeyHandle">The key handle as <see cref="IntPtr"/> from the backend to be inserted.</param>
        /// <param name="name">The name parameter as <see cref="string"/>.</param>
        /// <param name="metaData">The meta data parameter as <see cref="string"/>; default null.</param>
        /// <param name="tags">The tag parameter as <see cref="string"/>; default null.</param>
        /// <param name="expiryMs">The expiry in ms as <see cref="long"/>; default -1 which indicates no expiry.</param>
        /// <returns>The result of the insert key call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<bool> InsertKeyAsync(
            this Session session,
            IntPtr localKeyHandle,
            string name,
            string metaData = null,
            string tags = null,
            long expiryMs = -1 //None
            )
        {
            return session.sessionHandle == new IntPtr()
                ? throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot insert key with closed session")
                : await SessionInsertKeyAsync(session.sessionHandle, localKeyHandle, name, metaData, tags, expiryMs);
        }

        /// <summary>
        /// Fetch an existing key from the <see cref="Store"/> in the backend as a keyEntryList handle.
        /// </summary>
        /// <param name="session">The <see cref="Session"/> instance, holding the store and session handle from the backend.</param>
        /// <param name="name">The name parameter as <see cref="string"/>.</param>
        /// <param name="forUpdate">The <see cref="bool"/> flag. Specify `for_update` when in a transaction to create an update lock on the
        /// associated record, if supported by the store backend; default false.</param>
        /// <returns>A keyEntryList handle as <see cref="IntPtr"/> from the backend.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<IntPtr> FetchKeyAsync(this Session session, string name, bool forUpdate = false)
        {
            return session.sessionHandle == new IntPtr()
                ? throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot fetch key from closed session")
                : await SessionFetchKeyAsync(session.sessionHandle, name, forUpdate);
        }

        /// <summary>
        /// Fetch all existing keys matching the given filters from the <see cref="Store"/> in the backend as a keyEntryList handle.
        /// </summary>
        /// <param name="session">The <see cref="Session"/> instance, holding the store and session handle from the backend.</param>
        /// <param name="keyAlg">The key algorithm as <see cref="KeyAlg"/>; default none.</param>
        /// <param name="thumbprint">The thumbprint parameter as <see cref="string"/>; default null.</param>
        /// <param name="tagFilter">The tag filter parameter as <see cref="string"/>; default null.</param>
        /// <param name="limit">A limit parameter as <see cref="long"/> to limit the number of the total received results; default -1 which indicates no limit.</param>
        /// <param name="forUpdate">The <see cref="bool"/> flag. Specify `for_update` when in a transaction to create an update lock on the
        /// associated record, if supported by the store backend; default false.</param>
        /// <returns>A keyEntryList handle as <see cref="IntPtr"/> from the backend.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<IntPtr> FetchAllKeysAsync(
            this Session session,
            KeyAlg keyAlg = KeyAlg.NONE,
            string thumbprint = null,
            string tagFilter = null,
            long limit = -1, //None
            bool forUpdate = false)
        {
            return session.sessionHandle == new IntPtr()
                ? throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot fetch key from closed session")
                : await SessionFetchAllKeysAsync(session.sessionHandle, keyAlg, thumbprint, tagFilter, limit, forUpdate);
        }

        /// <summary>
        /// Replace the metadata and tags on an existing key in the <see cref="Store"/> in the backend.
        /// </summary>
        /// <param name="session">The <see cref="Session"/> instance, holding the store and session handle from the backend.</param>
        /// <param name="name">The name parameter as <see cref="string"/>.</param>
        /// <param name="metaData">The meta data parameter as <see cref="string"/>; default null.</param>
        /// <param name="tags">The tag parameter as <see cref="string"/>; default null.</param>
        /// <param name="expiryMs">The expiry in ms as <see cref="long"/>; default -1 which indicates no expiry.</param>
        /// <returns>The result of the update key call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<bool> UpdateKeyAsync(
            this Session session,
            string name,
            string metaData = null,
            string tags = null,
            long expiryMs = -1 //None
            )
        {
            return session.sessionHandle == new IntPtr()
                ? throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot update key with closed session")
                : await SessionUpdateKeyAsync(session.sessionHandle, name, metaData, tags, expiryMs);
        }

        /// <summary>
        /// Remove an existing key from the <see cref="Store"/> in the backend.
        /// </summary>
        /// <param name="session">The <see cref="Session"/> instance, holding the store and session handle from the backend.</param>
        /// <param name="name">The name parameter as <see cref="string"/>.</param>
        /// <returns>The result of the remove key call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        public static async Task<bool> RemoveKeyAsync(
            this Session session,
            string name)
        {
            return session.sessionHandle == new IntPtr()
                ? throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Cannot remove key with closed session")
                : await SessionRemoveKeyAsync(session.sessionHandle, name);
        }

        /// <summary>
        /// Close and commit on a <see cref="Session"/> in the backend and reset the session handle.
        /// </summary>
        /// <param name="session">The <see cref="Session"/> instance, holding the store and session handle from the backend.</param>
        /// <returns>The result of the close and commit call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
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
            else
            {
                throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Session is null");
            }
        }

        /// <summary>
        /// Close and rollback on a <see cref="Session"/> in the backend and reset the session handle.
        /// </summary>
        /// <param name="session">The <see cref="Session"/> instance, holding the store and session handle from the backend.</param>
        /// <returns>The result of the close and rollback call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
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
            else
            {
                throw AriesAskarException.FromWrapperError(ErrorCode.Wrapper, "Session is null");
            }
        }
        #endregion

        #region Store
        /// <summary>
        /// Generates and returns a new raw key from a given seed.
        /// </summary>
        /// <param name="seed">A seed phrase with a length of 32.</param>
        /// <returns>A random raw key in <see cref="string"/> format, created from a given seed.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        private static async Task<string> StoreGenerateRawKeyAsync(string seed)
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

        /// <summary>
        /// Provision a new <see cref="Store"/>.
        /// </summary>
        /// <param name="specUri">The provison spec uri <see cref="string"/>.</param>
        /// <param name="keyMethod">The key method from enum <see cref="KeyMethod"/>, which represents supported methods for generating or referencing a new store key; default none.</param>
        /// <param name="passKey">The pass key <see cref="string"/>, a possible empty password or key used to derive a store key; default null.</param>
        /// <param name="profile">The store profile <see cref="string"/>; default null.</param>
        /// <param name="recreate">The recreate <see cref="bool"/> flag; default false.</param>
        /// <returns>A new instance of <see cref="Store"/> containing the spec uri and store handle from rust sdk call.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<Store> StoreProvisionAsync(
            string specUri,
            KeyMethod keyMethod = KeyMethod.NONE,
            string passKey = null,
            string profile = null,
            bool recreate = false)
        {
            TaskCompletionSource<IntPtr> taskCompletionSource = new TaskCompletionSource<IntPtr>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

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

        /// <summary>
        /// Open an existing <see cref="Store"/>.
        /// </summary>
        /// <param name="specUri">The spec uri <see cref="string"/> of the store.</param>
        /// <param name="keyMethod">The key method from enum <see cref="KeyMethod"/>, which represents supported methods for generating or referencing a new store key; default none.</param>
        /// <param name="passKey">The pass key <see cref="string"/>, a possible empty password or key used to derive a store key; default null.</param>
        /// <param name="profile">The store profile <see cref="string"/>; default null.</param>
        /// <returns>The store instance of the existing <see cref="Store"/> containing the spec uri and store handle from rust sdk call.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<Store> StoreOpenAsync(
            string specUri,
            KeyMethod keyMethod = KeyMethod.NONE,
            string passKey = null,
            string profile = null)
        {
            TaskCompletionSource<IntPtr> taskCompletionSource = new TaskCompletionSource<IntPtr>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

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

        /// <summary>
        /// Remove an existing store from the backend.
        /// </summary>
        /// <param name="specUri">The spec uri <see cref="string"/> of the store to be removed.</param>
        /// <returns>The result of the remove call as <see cref="bool"/>; true if succeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<bool> StoreRemoveAsync(string specUri)
        {
            TaskCompletionSource<byte> taskCompletionSource = new TaskCompletionSource<byte>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

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

        /// <summary>
        /// Create a new profile with the given profile name in the backend. A random name is used if profile is null.
        /// </summary>
        /// <param name="profile">The profile name as <see cref="string"/>; default null.</param>
        /// <param name="storeHandle">The store handle as <see cref="IntPtr"/> from the backend.</param>
        /// <returns>The profile name of the created profile as <see cref="string"/>; if profile name is null, a random name is used by the backend.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<string> StoreCreateProfileAsync(IntPtr storeHandle, string profile = null)
        {
            TaskCompletionSource<string> taskCompletionSource = new TaskCompletionSource<string>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

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

        /// <summary>
        /// Get the default profile name used when starting a scan or a session.
        /// </summary>
        /// <param name="storeHandle">The store handle as <see cref="IntPtr"/> from the backend.</param>
        /// <returns>The default profile name as <see cref="string"/></returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<string> StoreGetProfileNameAsync(IntPtr storeHandle)
        {
            TaskCompletionSource<string> taskCompletionSource = new TaskCompletionSource<string>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

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

        /// <summary>
        /// Remove an existing profile from the backend with the given profile name.
        /// </summary>
        /// <param name="profile">The profile name of the profile to remove as <see cref="string"/>.</param>
        /// <param name="storeHandle">The store handle as <see cref="IntPtr"/> from the backend.</param>
        /// <returns>The result of the remove call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<bool> StoreRemoveProfileAsync(IntPtr storeHandle, string profile)
        {
            TaskCompletionSource<byte> taskCompletionSource = new TaskCompletionSource<byte>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

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

        /// <summary>
        /// Replace the wrapping key on a store in the backend.
        /// </summary>
        /// <param name="storeHandle">The store handle as <see cref="IntPtr"/> from the backend.</param>
        /// <param name="keyMethod">The key method from enum <see cref="KeyMethod"/>, which represents supported methods for generating or referencing a new store key; default none.</param>
        /// <param name="passKey">The pass key as <see cref="string"/>, a possible empty password or key used to derive a store key; default null.</param>
        /// <returns>The result of the rekey call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<bool> StoreRekeyAsync(
            IntPtr storeHandle,
            KeyMethod keyMethod = KeyMethod.NONE,
            string passKey = null)
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

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

        /// <summary>
        /// Close an existing open store from the backend.
        /// </summary>
        /// <param name="storeHandle">The store handle as <see cref="IntPtr"/> of the store to be closed in the backend.</param>
        /// <returns>The result of the close call as <see cref="bool"/>; true if succeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<bool> StoreCloseAsync(IntPtr storeHandle)
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

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
        /// <summary>
        /// Create a new <see cref="Scan"/> instance against the store in the backend. The result will keep an open connection to the backend until it is consumed.
        /// </summary>
        /// <param name="storeHandle">The store handle as <see cref="IntPtr"/> to scan against from the backend.</param>
        /// <param name="category">The category filter parameter as <see cref="string"/>.</param>
        /// <param name="tagFilter">The tag filter parameter as <see cref="string"/>; default null.</param>
        /// <param name="offset">A offset parameter as <see cref="long"/> to offset a number of the total results; default 0.</param>
        /// <param name="limit">>A limit parameter as <see cref="long"/> to limit the number of the total received results; default -1 which indicates no limit.</param>
        /// <param name="profile">The profile name filter parameter as <see cref="string"/>; default null.</param>
        /// <returns>A new <see cref="Scan"/> instance containing the scan handle, store handle of the backend and the given parameters.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<Scan> StartScanAsync(
            IntPtr storeHandle,
            string category,
            string tagFilter = null,
            long offset = 0,
            long limit = -1, //None 
            string profile = null)
        {
            TaskCompletionSource<IntPtr> taskCompletionSource = new TaskCompletionSource<IntPtr>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

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
            List<object> parameters = new List<object>() { profile, category, tagFilter, offset, limit };
            return new Scan(await taskCompletionSource.Task, storeHandle, parameters);
        }

        /// <summary>
        /// Fetch the results for the given <see cref="Scan"/> object as an entryListHandle from the backend.
        /// </summary>
        /// <param name="scanHandle">The scan handle as <see cref="IntPtr"/> from the backend.</param>
        /// <returns>An entryListHandle as <see cref="IntPtr"/> which can be used as input for the methods in <see cref="ResultListApi"/> to obtain parameters from the records in the store.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<IntPtr> ScanNextAsync(IntPtr scanHandle)
        {
            TaskCompletionSource<IntPtr> taskCompletionSource = new TaskCompletionSource<IntPtr>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

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

        /// <summary>
        /// Free the scan object in the backend.
        /// </summary>
        /// <param name="scanHandle">The scan handle as <see cref="IntPtr"/> from the backend.</param>
        /// <returns>The result of the free call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
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
            {
                return true;
            }
        }
        #endregion

        #region Session
        /// <summary>
        /// Create a new <see cref="Session"/> instance against the store in the backend with a given profile name as session or transaction.
        /// </summary>
        /// <param name="storeHandle">The store handle as <see cref="IntPtr"/> from the backend.</param>
        /// <param name="profile">The profile name of the session / transaction as <see cref="string"/>. A random name is used by the backend when null; default null.</param>
        /// <param name="asTransactions">The <see cref="bool"/> flag, which indicates a session (false) or transaction (true) ; default false.</param>
        /// <returns>A new <see cref="Session"/> instance containing the store handle, session handle of the backend and the asTransaction and profile parameters.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<Session> SessionStartAsync(IntPtr storeHandle, string profile = null, bool asTransactions = false)
        {
            TaskCompletionSource<IntPtr> taskCompletionSource = new TaskCompletionSource<IntPtr>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

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

        /// <summary>
        /// Count the number of entries for a given record category.
        /// </summary>
        /// <param name="sessionHandle">The session handle as <see cref="IntPtr"/> from the backend.</param>
        /// <param name="category">The category parameter as <see cref="string"/>.</param>
        /// <param name="tagFilter">The tag filter parameter as <see cref="string"/>; default null.</param>
        /// <returns>The number of entries as <see cref="long"/> matching the given category and tag filter.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<long> SessionCountAsync(IntPtr sessionHandle, string category, string tagFilter = null)
        {
            TaskCompletionSource<long> taskCompletionSource = new TaskCompletionSource<long>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

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

        /// <summary>
        /// Fetch the current record for a given category and name from the backend as an entryListHandle.
        /// </summary>
        /// <param name="sessionHandle">The session handle as <see cref="IntPtr"/> from the backend.</param>
        /// <param name="category">The category parameter as <see cref="string"/>.</param>
        /// <param name="name">The name parameter as <see cref="string"/>.</param>
        /// <param name="forUpdate">The <see cref="bool"/> flag. Specify `for_update` when in a transaction to create an update lock on the
        /// associated record, if supported by the store backend; default false.</param>
        /// <returns>A entryList handle as <see cref="IntPtr"/> from the backend.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<IntPtr> SessionFetchAsync(IntPtr sessionHandle, string category, string name, bool forUpdate = false)
        {
            TaskCompletionSource<IntPtr> taskCompletionSource = new TaskCompletionSource<IntPtr>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

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

        /// <summary>
        /// Fetch all records matching the given category and tag filter parameters from the backend as an entryListHandle. This method may be used within a transaction. It should
        /// not be used for very large result sets due to correspondingly large memory requirements.
        /// </summary>
        /// <param name="sessionHandle">The session handle as <see cref="IntPtr"/> from the backend.</param>
        /// <param name="category">The category parameter as <see cref="string"/>.</param>
        /// <param name="tagFilter">The tag filter parameter as <see cref="string"/>.</param>
        /// <param name="limit">A limit parameter as <see cref="long"/> to limit the number of the total received results; default -1 which indicates no limit.</param>
        /// <param name="forUpdate">The <see cref="bool"/> flag. Specify `for_update` when in a transaction to create an update lock on the
        /// associated record, if supported by the store backend; default false.</param>
        /// <returns>A entryList handle as <see cref="IntPtr"/> from the backend.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<IntPtr> SessionFetchAllAsync(
            IntPtr sessionHandle,
            string category,
            string tagFilter = null,
            long limit = -1, //None 
            bool forUpdate = false)
        {
            TaskCompletionSource<IntPtr> taskCompletionSource = new TaskCompletionSource<IntPtr>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

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

        /// <summary>
        /// Remove all records from the store in the backend matching the given category and tag filter.
        /// </summary>
        /// <param name="sessionHandle">The session handle as <see cref="IntPtr"/> from the backend.</param>
        /// <param name="category">The category parameter as <see cref="string"/>.</param>
        /// <param name="tagFilter">The tag filter parameter as <see cref="string"/>; default null.</param>
        /// <returns>The number of removed records as <see cref="long"/>.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<long> SessionRemoveAllAsync(IntPtr sessionHandle, string category, string tagFilter = null)
        {
            TaskCompletionSource<long> taskCompletionSource = new TaskCompletionSource<long>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

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

        /// <summary>
        /// Insert a new record into the store in the backend.
        /// </summary>
        /// <param name="sessionHandle">The session handle as <see cref="IntPtr"/> from the backend.</param>
        /// <param name="category">The category parameter as <see cref="string"/>.</param>
        /// <param name="name">The name parameter as <see cref="string"/>.</param>
        /// <param name="value">The value parameter as <see cref="string"/>; default null.</param>
        /// <param name="tags">The tag parameter as <see cref="string"/>; default null.</param>
        /// <param name="expiryMs">The expiry in ms as <see cref="long"/>; default -1 which indicates no expiry.</param>
        /// <returns>The result of the insert call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
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

        /// <summary>
        /// Replace the value and tags of a record in the <see cref="Store"/> in the backend.
        /// </summary>
        /// <param name="sessionHandle">The session handle as <see cref="IntPtr"/> from the backend.</param>
        /// <param name="category">The category parameter as <see cref="string"/>.</param>
        /// <param name="name">The name parameter as <see cref="string"/>.</param>
        /// <param name="value">The value parameter as <see cref="string"/>; default null.</param>
        /// <param name="tags">The tag parameter as <see cref="string"/>; default null.</param>
        /// <param name="expiryMs">The expiry in ms as <see cref="long"/>; default -1 which indicates no expiry.</param>
        /// <returns>The result of the replace call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
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

        /// <summary>
        /// Remove a record from the store in the backend matching the given category and name.
        /// </summary>
        /// <param name="sessionHandle">The session handle as <see cref="IntPtr"/> from the backend.</param>
        /// <param name="category">The category parameter as <see cref="string"/>.</param>
        /// <param name="name">The name parameter as <see cref="string"/>.</param>
        /// <returns>The result of the remove call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
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

        /// <summary>
        /// Insert, replace or remove the value and tags of a record in the <see cref="Store"/> in the backend.
        /// </summary>
        /// <param name="sessionHandle">The session handle as <see cref="IntPtr"/> from the backend.</param>
        /// <param name="operation">The update operation as <see cref="UpdateOperation"/> to perfom on the store in the backend. Valid operations are insert, replace or remove.</param>
        /// <param name="category">The category parameter as <see cref="string"/>.</param>
        /// <param name="name">The name parameter as <see cref="string"/>.</param>
        /// <param name="value">The value parameter as <see cref="string"/>; default null.</param>
        /// <param name="tags">The tag parameter as <see cref="string"/>; default null.</param>
        /// <param name="expiryMs">The expiry in ms as <see cref="long"/>; default -1 which indicates no expiry.</param>
        /// <returns>The result of the update call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
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
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

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

        /// <summary>
        /// Insert a local key instance into the <see cref="Store"/> in the backend.
        /// </summary>
        /// <param name="sessionHandle">The session handle as <see cref="IntPtr"/> from the backend.</param>
        /// <param name="localKeyHandle">The key handle as <see cref="IntPtr"/> from the backend to be inserted.</param>
        /// <param name="name">The name parameter as <see cref="string"/>.</param>
        /// <param name="metaData">The meta data parameter as <see cref="string"/>; default null.</param>
        /// <param name="tags">The tag parameter as <see cref="string"/>; default null.</param>
        /// <param name="expiryMs">The expiry in ms as <see cref="long"/>; default -1 which indicates no expiry.</param>
        /// <returns>The result of the insert key call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<bool> SessionInsertKeyAsync(
            IntPtr sessionHandle,
            IntPtr localKeyHandle,
            string name,
            string metaData = null,
            string tags = null,
            long expiryMs = -1 //None
            )
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

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

        /// <summary>
        /// Fetch an existing key from the <see cref="Store"/> in the backend as a keyEntryList handle.
        /// </summary>
        /// <param name="sessionHandle">The session handle as <see cref="IntPtr"/> from the backend.</param>
        /// <param name="name">The name parameter as <see cref="string"/>.</param>
        /// <param name="forUpdate">The <see cref="bool"/> flag. Specify `for_update` when in a transaction to create an update lock on the
        /// associated record, if supported by the store backend; default false.</param>
        /// <returns>A keyEntryList handle as <see cref="IntPtr"/> from the backend.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<IntPtr> SessionFetchKeyAsync(IntPtr sessionHandle, string name, bool forUpdate = false)
        {
            TaskCompletionSource<IntPtr> taskCompletionSource = new TaskCompletionSource<IntPtr>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

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

        /// <summary>
        /// Fetch all existing keys matching the given filters from the <see cref="Store"/> in the backend as a keyEntryList handle.
        /// </summary>
        /// <param name="sessionHandle">The session handle as <see cref="IntPtr"/> from the backend.</param>
        /// <param name="keyAlg">The key algorithm as <see cref="KeyAlg"/>; default none.</param>
        /// <param name="thumbprint">The thumbprint parameter as <see cref="string"/>; default null.</param>
        /// <param name="tagFilter">The tag filter parameter as <see cref="string"/>; default null.</param>
        /// <param name="limit">A limit parameter as <see cref="long"/> to limit the number of the total received results; default -1 which indicates no limit.</param>
        /// <param name="forUpdate">The <see cref="bool"/> flag. Specify `for_update` when in a transaction to create an update lock on the
        /// associated record, if supported by the store backend; default false.</param>
        /// <returns>A keyEntryList handle as <see cref="IntPtr"/> from the backend.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<IntPtr> SessionFetchAllKeysAsync(
            IntPtr sessionHandle,
            KeyAlg keyAlg = KeyAlg.NONE,
            string thumbprint = null,
            string tagFilter = null,
            long limit = -1, //None 
            bool forUpdate = false)
        {
            TaskCompletionSource<IntPtr> taskCompletionSource = new TaskCompletionSource<IntPtr>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

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

        /// <summary>
        /// Replace the metadata and tags on an existing key in the store in the backend.
        /// </summary>
        /// <param name="sessionHandle">The session handle as <see cref="IntPtr"/> from the backend.</param>
        /// <param name="name">The name parameter as <see cref="string"/>.</param>
        /// <param name="metaData">The meta data parameter as <see cref="string"/>; default null.</param>
        /// <param name="tags">The tag parameter as <see cref="string"/>; default null.</param>
        /// <param name="expiryMs">The expiry in ms as <see cref="long"/>; default -1 which indicates no expiry.</param>
        /// <returns>The result of the update key call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<bool> SessionUpdateKeyAsync(
            IntPtr sessionHandle,
            string name,
            string metaData = null,
            string tags = null,
            long expiryMs = -1 //None
            )
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

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

        /// <summary>
        /// Remove an existing key from the store in the backend.
        /// </summary>
        /// <param name="sessionHandle">The <see cref="Session"/> instance, holding the store and session handle from the backend.</param>
        /// <param name="name">The name parameter as <see cref="string"/>.</param>
        /// <returns>The result of the remove key call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<bool> SessionRemoveKeyAsync(IntPtr sessionHandle, string name)
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

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

        /// <summary>
        /// Close and commit on a <see cref="Session"/> in the backend and reset the session handle.
        /// </summary>
        /// <param name="sessionHandle">The <see cref="Session"/> instance, holding the store and session handle from the backend.</param>
        /// <returns>The result of the close and commit call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<bool> SessionCloseAndCommitAsync(IntPtr sessionHandle)
        {
            bool res = await SessionCloseAsync(
                sessionHandle,
                true);

            return await Task.FromResult(res);
        }

        /// <summary>
        /// Close and rollback on a <see cref="Session"/> in the backend and reset the session handle.
        /// </summary>
        /// <param name="sessionHandle">The <see cref="Session"/> instance, holding the store and session handle from the backend.</param>
        /// <returns>The result of the close and rollback call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<bool> SessionCloseAndRollbackAsync(IntPtr sessionHandle)
        {
            bool res = await SessionCloseAsync(
                sessionHandle,
                false);

            return await Task.FromResult(res);
        }

        /// <summary>
        /// Close a <see cref="Session"/> in the backend and perform a commit or rollback.
        /// </summary>
        /// <param name="sessionHandle">The <see cref="Session"/> instance, holding the store and session handle from the backend.</param>
        /// <param name="commit">The <see cref="bool"/> flag, which indicates that there is a commit or rollback on the session; true commit, false rollback.</param>
        /// <returns>The result of the close call as <see cref="bool"/>; true if succeeded, otherwise false.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static async Task<bool> SessionCloseAsync(IntPtr sessionHandle, bool commit)
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

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
        /// <summary>
        /// 'No value back' callback method that is handed to the backend call.
        /// </summary>
        /// <param name="callback_id">The callback id.</param>
        /// <param name="err">The received <see cref="ErrorCode"/> of the backend call.</param>
        /// <returns>An <see cref="AriesAskarException"/> if the <see cref="ErrorCode"/> is not <see cref="ErrorCode.Success"/>, otherwise true.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static void NoReturnValueStoreCallbackMethod(long callback_id, int err)
        {
            TaskCompletionSource<bool> taskCompletionSource = PendingCallbacks.Remove<bool>(callback_id);

            if (err != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                taskCompletionSource.SetException(AriesAskarException.FromSdkError(error));
                return;
            }
            taskCompletionSource.SetResult(true);
        }
        private static readonly NoReturnValueStoreCompletedDelegate NoReturnValueStoreCallback = NoReturnValueStoreCallbackMethod;

        /// <summary>
        /// 'Get handle back' callback method that is handed to the backend call.
        /// </summary>
        /// <param name="callback_id">The callback id.</param>
        /// <param name="err">The received <see cref="ErrorCode"/> of the backend call.</param>
        /// <param name="handle">The received handle as <see cref="IntPtr"/> of the backend call.</param>
        /// <returns>An <see cref="AriesAskarException"/> if the <see cref="ErrorCode"/> is not <see cref="ErrorCode.Success"/>, otherwise a object handle as <see cref="IntPtr"/>.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static void GetStoreHandleCallbackMethod(long callback_id, int err, IntPtr handle)
        {
            TaskCompletionSource<IntPtr> taskCompletionSource = PendingCallbacks.Remove<IntPtr>(callback_id);

            if (err != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                taskCompletionSource.SetException(AriesAskarException.FromSdkError(error));
                return;
            }
            taskCompletionSource.SetResult(handle);
        }
        private static readonly GetStoreHandleCompletedDelegate GetStoreHandleCallback = GetStoreHandleCallbackMethod;

        /// <summary>
        /// 'Get <see cref="byte"/> back' callback method that is handed to the backend call.
        /// </summary>
        /// <param name="callback_id">The callback id.</param>
        /// <param name="err">The received <see cref="ErrorCode"/> of the backend call.</param>
        /// <param name="result">The received <see cref="byte"/> result of the backend call.</param>
        /// <returns>An <see cref="AriesAskarException"/> if the <see cref="ErrorCode"/> is not <see cref="ErrorCode.Success"/>, otherwise a result <see cref="byte"/>.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static void GetStoreByteCallbackMethod(long callback_id, int err, byte result)
        {
            TaskCompletionSource<byte> taskCompletionSource = PendingCallbacks.Remove<byte>(callback_id);

            if (err != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                taskCompletionSource.SetException(AriesAskarException.FromSdkError(error));
                return;
            }
            taskCompletionSource.SetResult(result);
        }
        private static readonly GetStoreByteCompletedDelegate GetStoreByteCallback = GetStoreByteCallbackMethod;

        /// <summary>
        /// 'Get <see cref="long"/> back' callback method that is handed to the backend call.
        /// </summary>
        /// <param name="callback_id">The callback id.</param>
        /// <param name="err">The received <see cref="ErrorCode"/> of the backend call.</param>
        /// <param name="result">The received <see cref="long"/> result of the backend call.</param>
        /// <returns>An <see cref="AriesAskarException"/> if the <see cref="ErrorCode"/> is not <see cref="ErrorCode.Success"/>, otherwise a result <see cref="long"/>.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static void GetStoreLongCallbackMethod(long callback_id, int err, long result)
        {
            TaskCompletionSource<long> taskCompletionSource = PendingCallbacks.Remove<long>(callback_id);

            if (err != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                taskCompletionSource.SetException(AriesAskarException.FromSdkError(error));
                return;
            }
            taskCompletionSource.SetResult(result);
        }
        private static readonly GetStoreLongCompletedDelegate GetStoreLongCallback = GetStoreLongCallbackMethod;

        /// <summary>
        /// 'Get <see cref="string"/> back' callback method that is handed to the backend call.
        /// </summary>
        /// <param name="callback_id">The callback id.</param>
        /// <param name="err">The received <see cref="ErrorCode"/> of the backend call.</param>
        /// <param name="result">The received <see cref="string"/> result of the backend call.</param>
        /// <returns>An <see cref="AriesAskarException"/> if the <see cref="ErrorCode"/> is not <see cref="ErrorCode.Success"/>, otherwise a result <see cref="string"/>.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter or backend throwing error. 
        /// </exception>
        private static void GetStoreStringCallbackMethod(long callback_id, int err, string result)
        {
            TaskCompletionSource<string> taskCompletionSource = PendingCallbacks.Remove<string>(callback_id);

            if (err != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                taskCompletionSource.SetException(AriesAskarException.FromSdkError(error));
                return;
            }
            taskCompletionSource.SetResult(result);
        }
        private static readonly GetStoreStringCompletedDelegate GetStoreStringCallback = GetStoreStringCallbackMethod;
        #endregion
    }
}