using System;
using System.Threading.Tasks;
using static indy_vdr_dotnet.models.Structures;

namespace aries_askar_dotnet.AriesAskar
{
    public class ResultListApi
    {
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

        public static async Task<ByteBuffer> EntryListGetValueAsync(
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
            return value;
        }

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

        public static async Task<bool> EntryListFreeAsync(
            IntPtr entryListHandle)
        {
            int errorCode = NativeMethods.askar_entry_list_free(
                entryListHandle);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            else
                return true;
        }

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

        public static async Task<bool> KeyEntryListFreeAsync(
            IntPtr keyEntryListHandle)
        {
            int errorCode = NativeMethods.askar_key_entry_list_free(
                keyEntryListHandle);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            else
                return true;
        }

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

        public static async Task<IntPtr> KeyEntryListLoadLocalAsync(
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
