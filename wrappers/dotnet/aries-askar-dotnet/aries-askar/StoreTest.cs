using aries_askar_dotnet.Models;
using aries_askar_dotnet.utils;
using System;
using System.Threading.Tasks;
using static aries_askar_dotnet.aries_askar.NativeMethods;
using static indy_vdr_dotnet.models.Structures;

namespace aries_askar_dotnet.aries_askar
{
    public class StoreTest
    {
        public uint _storeHandle {get;set;}
        public StoreTest(uint storeHandle)
        {
            _storeHandle = storeHandle;
        }

        public async Task<string> StoreGenerateRawKeyAsync(string seed)
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

        public static async Task<StoreTest> StoreProvisionAsync(
            string specUri,
            KeyMethod keyMethod = KeyMethod.NONE,
            string passKey = null,
            string profile = null,
            bool recreate = false)
        {
            var taskCompletionSource = new TaskCompletionSource<uint>();
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

            return new StoreTest(await taskCompletionSource.Task);
        }
        
        public async Task<bool> StoreRemoveProfileAsync(StoreTest store, string profile)
        {
            var taskCompletionSource = new TaskCompletionSource<byte>();
            var callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_store_remove_profile(
                store._storeHandle,
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
    }
}