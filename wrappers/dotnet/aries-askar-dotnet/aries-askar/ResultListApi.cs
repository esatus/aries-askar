using System;
using System.Threading.Tasks;
using static indy_vdr_dotnet.models.Structures;

namespace aries_askar_dotnet.aries_askar
{
    public class ResultListApi
    {
        public static async Task<int> EntryListCountAsync(
            uint entryListHandle)
        {
            int count = 0;
            int errorCode = NativeMethods.askar_entry_list_count(
                entryListHandle,
                ref count);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }
            return count;
        }

        public static async Task<string> EntryListGetCategoryAsync(
            uint entryListHandle,
            int index)
        {
            string category = "";
            int errorCode = NativeMethods.askar_entry_list_get_category(
                entryListHandle,
                index,
                category);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }
            return category;
        }

        public static async Task<string> EntryListGetNameAsync(
            uint entryListHandle,
            int index)
        {
            string name = "";
            int errorCode = NativeMethods.askar_entry_list_get_name(
                entryListHandle,
                index,
                name);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }
            return name;
        }

        public static async Task<SecretBuffer> EntryListGetValueAsync(
            uint entryListHandle,
            int index)
        {
            SecretBuffer value = new SecretBuffer();
            int errorCode = NativeMethods.askar_entry_list_get_value(
                entryListHandle,
                index,
                ref value);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }
            return value;
        }

        public static async Task<string> EntryListGetTagsAsync(
            uint entryListHandle,
            int index)
        {
            string tags = "";
            int errorCode = NativeMethods.askar_entry_list_get_tags(
                entryListHandle,
                index,
                tags);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }
            return tags;
        }

        public static async Task EntryListFreeAsync(
            uint entryListHandle)
        {
            int errorCode = NativeMethods.askar_entry_list_free(
                entryListHandle);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }
        }

        public static async Task<int> KeyEntryListCountAsync(
            uint keyEntryListHandle)
        {
            int count = 0;
            int errorCode = NativeMethods.askar_key_entry_list_count(
                keyEntryListHandle,
                ref count);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }
            return count;
        }

        public static async Task KeyEntryListFreeAsync(
            uint keyEntryListHandle)
        {
            int errorCode = NativeMethods.askar_key_entry_list_free(
                keyEntryListHandle);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }
        }

        public static async Task<string> KeyEntryListGetAlgorithmAsync(
            uint keyEntryListHandle,
            int index)
        {
            string alg = "";
            int errorCode = NativeMethods.askar_key_entry_list_get_algorithm(
                keyEntryListHandle,
                index,
                alg);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }
            return alg;
        }

        public static async Task<string> KeyEntryListGetNameAsync(
            uint keyEntryListHandle,
            int index)
        {
            string name = "";
            int errorCode = NativeMethods.askar_key_entry_list_get_name(
                keyEntryListHandle,
                index,
                name);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }
            return name;
        }

        public static async Task<string> KeyEntryListGetMetadataAsync(
            uint keyEntryListHandle,
            int index)
        {
            string metadata = "";
            int errorCode = NativeMethods.askar_key_entry_list_get_metadata(
                keyEntryListHandle,
                index,
                metadata);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }
            return metadata;
        }

        public static async Task<string> KeyEntryListgetTagsyAsync(
            uint keyEntryListHandle,
            int index)
        {
            string tags = "";
            int errorCode = NativeMethods.askar_key_entry_list_get_tags(
                keyEntryListHandle,
                index,
                tags);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }
            return tags;
        }

        public static async Task<uint> KeyEntryListLoadLocalAsync(
            uint keyEntryListHandle,
            int index)
        {
            uint output = 0;
            int errorCode = NativeMethods.askar_key_entry_list_load_local(
                keyEntryListHandle,
                index,
                ref output);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }
            return output;
        }
    }
}
