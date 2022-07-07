using System;
using System.Threading.Tasks;
using static indy_vdr_dotnet.models.Structures;

namespace aries_askar_dotnet.AriesAskar
{
    public class ResultListApi
    {
        /// <summary>
        /// Count the number of record entries for a given entrylist handle from the backend.
        /// </summary>
        /// <param name="entryListHandle">The entrylist handle as <see cref="IntPtr"/> which is received from the 
        /// <see cref="StoreApi.FetchAsync(Models.Session, string, string, bool)"/> and <see cref="StoreApi.FetchAllAsync(Models.Session, string, string, long, bool)"/> calls.</param>
        /// <returns>The number of fetched record entries as <see cref="int"/> for a given entrylist handle.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<int> EntryListCountAsync(
            IntPtr entryListHandle)
        {
            int count = 0;
            int errorCode = NativeMethods.askar_entry_list_count(
                entryListHandle,
                ref count);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return count;
        }

        /// <summary>
        /// Get the category of one or more rows of record entries for a given entrylist handle from the backend.
        /// </summary>
        /// <param name="entryListHandle">The entrylist handle as <see cref="IntPtr"/> which is received from the 
        /// <see cref="StoreApi.FetchAsync(Models.Session, string, string, bool)"/> and <see cref="StoreApi.FetchAllAsync(Models.Session, string, string, long, bool)"/> calls.</param>
        /// <param name="index">The row index for a entry as <see cref="int"/>.</param>
        /// <returns>The category of a record entry at a given index as <see cref="string"/> for a given entrylist handle.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<string> EntryListGetCategoryAsync(
            IntPtr entryListHandle,
            int index)
        {
            string category = "";
            int errorCode = NativeMethods.askar_entry_list_get_category(
                entryListHandle,
                index,
                ref category);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return category;
        }

        /// <summary>
        /// Get the name of one or more rows of record entries for a given entrylist handle from the backend.
        /// </summary>
        /// <param name="entryListHandle">The entrylist handle as <see cref="IntPtr"/> which is received from the 
        /// <see cref="StoreApi.FetchAsync(Models.Session, string, string, bool)"/> and <see cref="StoreApi.FetchAllAsync(Models.Session, string, string, long, bool)"/> calls.</param>
        /// <param name="index">The row index for a entry as <see cref="int"/>.</param>
        /// <returns>The name of a record entry at a given index as <see cref="string"/> for a given entrylist handle.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<string> EntryListGetNameAsync(
            IntPtr entryListHandle,
            int index)
        {
            string name = "";
            int errorCode = NativeMethods.askar_entry_list_get_name(
                entryListHandle,
                index,
                ref name);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return name;
        }

        /// <summary>
        /// Get the value of one or more rows of record entries for a given entrylist handle from the backend.
        /// </summary>
        /// <param name="entryListHandle">The entrylist handle as <see cref="IntPtr"/> which is received from the 
        /// <see cref="StoreApi.FetchAsync(Models.Session, string, string, bool)"/> and <see cref="StoreApi.FetchAllAsync(Models.Session, string, string, long, bool)"/> calls.</param>
        /// <param name="index">The row index for a entry as <see cref="int"/>.</param>
        /// <returns>The value of a record entry at a given index as <see cref="string"/> for a given entrylist handle.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<string> EntryListGetValueAsync(
            IntPtr entryListHandle,
            int index)
        {
            ByteBuffer value = new ByteBuffer();
            int errorCode = NativeMethods.askar_entry_list_get_value(
                entryListHandle,
                index,
                ref value);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return value.DecodeToString();
        }

        /// <summary>
        /// Get the tag of one or more rows of record entries for a given entrylist handle from the backend.
        /// </summary>
        /// <param name="entryListHandle">The entrylist handle as <see cref="IntPtr"/> which is received from the 
        /// <see cref="StoreApi.FetchAsync(Models.Session, string, string, bool)"/> and <see cref="StoreApi.FetchAllAsync(Models.Session, string, string, long, bool)"/> calls.</param>
        /// <param name="index">The row index for a entry as <see cref="int"/>.</param>
        /// <returns>The tag of a record entry at a given index as <see cref="string"/> for a given entrylist handle.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<string> EntryListGetTagsAsync(
            IntPtr entryListHandle,
            int index)
        {
            string tags = "";
            int errorCode = NativeMethods.askar_entry_list_get_tags(
                entryListHandle,
                index,
                ref tags);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return tags;
        }

        /// <summary>
        /// Free the given entrylist handle in the backend.
        /// </summary>
        /// <param name="entryListHandle">The entrylist handle as <see cref="IntPtr"/> which is received from the 
        /// <see cref="StoreApi.FetchAsync(Models.Session, string, string, bool)"/> and <see cref="StoreApi.FetchAllAsync(Models.Session, string, string, long, bool)"/> calls.</param>
        public static Task EntryListFreeAsync(
            IntPtr entryListHandle)
        {
            NativeMethods.askar_entry_list_free(
                entryListHandle);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Count the number of key entries for a given keyEntrylist handle from the backend.
        /// </summary>
        /// <param name="keyEntryListHandle">The keyEntrylist handle as <see cref="IntPtr"/> which is received from the 
        /// <see cref="StoreApi.FetchKeyAsync(Models.Session, string, bool)"/> and <see cref="StoreApi.FetchAllKeysAsync(Models.Session, Models.KeyAlg, string, string, long, bool)"/> calls.</param>
        /// <returns>The number of fetched key entries as <see cref="int"/> for a given keyEntrylist handle.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<int> KeyEntryListCountAsync(
            IntPtr keyEntryListHandle)
        {
            int count = 0;
            int errorCode = NativeMethods.askar_key_entry_list_count(
                keyEntryListHandle,
                ref count);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return count;
        }

        /// <summary>
        /// Free the given keyEntrylist handle in the backend.
        /// </summary>
        /// <param name="keyEntryListHandle">The keyEntrylist handle as <see cref="IntPtr"/> which is received from the 
        /// <see cref="StoreApi.FetchKeyAsync(Models.Session, string, bool)"/> and <see cref="StoreApi.FetchAllKeysAsync(Models.Session, Models.KeyAlg, string, string, long, bool)"/> calls.</param>
        public static Task KeyEntryListFreeAsync(
            IntPtr keyEntryListHandle)
        {
            NativeMethods.askar_key_entry_list_free(
                keyEntryListHandle);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Get the algorithm of one or more rows of key entries for a given keyEntrylist handle from the backend.
        /// </summary>
        /// <param name="keyEntryListHandle">The keyEntrylist handle as <see cref="IntPtr"/> which is received from the 
        /// <see cref="StoreApi.FetchKeyAsync(Models.Session, string, bool)"/> and <see cref="StoreApi.FetchAllKeysAsync(Models.Session, Models.KeyAlg, string, string, long, bool)"/> calls.</param>
        /// <param name="index">The row index for a key entry as <see cref="int"/>.</param>
        /// <returns>The algorithm of a key entry at a given index as <see cref="string"/> for a given keyEntrylist handle.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<string> KeyEntryListGetAlgorithmAsync(
            IntPtr keyEntryListHandle,
            int index)
        {
            string alg = "";
            int errorCode = NativeMethods.askar_key_entry_list_get_algorithm(
                keyEntryListHandle,
                index,
                ref alg);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return alg;
        }

        /// <summary>
        /// Get the name of one or more rows of key entries for a given keyEntrylist handle from the backend.
        /// </summary>
        /// <param name="keyEntryListHandle">The keyEntrylist handle as <see cref="IntPtr"/> which is received from the 
        /// <see cref="StoreApi.FetchKeyAsync(Models.Session, string, bool)"/> and <see cref="StoreApi.FetchAllKeysAsync(Models.Session, Models.KeyAlg, string, string, long, bool)"/> calls.</param>
        /// <param name="index">The row index for a key entry as <see cref="int"/>.</param>
        /// <returns>The name of a key entry at a given index as <see cref="string"/> for a given keyEntrylist handle.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<string> KeyEntryListGetNameAsync(
            IntPtr keyEntryListHandle,
            int index)
        {
            string name = "";
            int errorCode = NativeMethods.askar_key_entry_list_get_name(
                keyEntryListHandle,
                index,
                ref name);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return name;
        }

        /// <summary>
        /// Get the metadata of one or more rows of key entries for a given keyEntrylist handle from the backend.
        /// </summary>
        /// <param name="keyEntryListHandle">The keyEntrylist handle as <see cref="IntPtr"/> which is received from the 
        /// <see cref="StoreApi.FetchKeyAsync(Models.Session, string, bool)"/> and <see cref="StoreApi.FetchAllKeysAsync(Models.Session, Models.KeyAlg, string, string, long, bool)"/> calls.</param>
        /// <param name="index">The row index for a key entry as <see cref="int"/>.</param>
        /// <returns>The metadata of a key entry at a given index as <see cref="string"/> for a given keyEntrylist handle.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<string> KeyEntryListGetMetadataAsync(
            IntPtr keyEntryListHandle,
            int index)
        {
            string metadata = "";
            int errorCode = NativeMethods.askar_key_entry_list_get_metadata(
                keyEntryListHandle,
                index,
                ref metadata);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return metadata;
        }

        /// <summary>
        /// Get the tag of one or more rows of key entries for a given keyEntrylist handle from the backend.
        /// </summary>
        /// <param name="keyEntryListHandle">The keyEntrylist handle as <see cref="IntPtr"/> which is received from the 
        /// <see cref="StoreApi.FetchKeyAsync(Models.Session, string, bool)"/> and <see cref="StoreApi.FetchAllKeysAsync(Models.Session, Models.KeyAlg, string, string, long, bool)"/> calls.</param>
        /// <param name="index">The row index for a key entry as <see cref="int"/>.</param>
        /// <returns>The tag of a key entry at a given index as <see cref="string"/> for a given keyEntrylist handle.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<string> KeyEntryListGetTagsAsync(
            IntPtr keyEntryListHandle,
            int index)
        {
            string tags = "";
            int errorCode = NativeMethods.askar_key_entry_list_get_tags(
                keyEntryListHandle,
                index,
                ref tags);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return tags;
        }

        /// <summary>
        /// Get localKey handle as <see cref="IntPtr"/> of one or more rows of key entries for a given keyEntrylist handle from the backend.
        /// </summary>
        /// <param name="keyEntryListHandle">The keyEntrylist handle as <see cref="IntPtr"/> which is received from the 
        /// <see cref="StoreApi.FetchKeyAsync(Models.Session, string, bool)"/> and <see cref="StoreApi.FetchAllKeysAsync(Models.Session, Models.KeyAlg, string, string, long, bool)"/> calls.</param>
        /// <param name="index">The row index for a key entry as <see cref="int"/>.</param>
        /// <returns>The localKey handle of a key entry at a given index as <see cref="IntPtr"/> for a given keyEntrylist handle.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<IntPtr> LoadLocalKeyHandleFromKeyEntryListAsync(
            IntPtr keyEntryListHandle,
            int index)
        {
            IntPtr output = new IntPtr();
            int errorCode = NativeMethods.askar_key_entry_list_load_local(
                keyEntryListHandle,
                index,
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output;
        }
    }
}
