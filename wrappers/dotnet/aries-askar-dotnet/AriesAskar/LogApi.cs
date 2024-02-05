using aries_askar_dotnet.utils;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static aries_askar_dotnet.AriesAskar.NativeMethods;
using static aries_askar_dotnet.Models.Structures;

namespace aries_askar_dotnet.AriesAskar
{
    public static class LogApi
    {
        public static async Task<int> SetCustomLoggerAsync(IntPtr context, int max_level)
        {

            int errorCode = NativeMethods.askar_set_custom_logger(context, GetLogCompletedCallback, GetEnableCompletedCallback, GetFlushCompletedCallback, max_level);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                throw AriesAskarException.FromSdkError(error);
            }

            return errorCode;
        }

        private static void GetLogCompletedCallbackMethod(IntPtr context, int level, string target, string message, string module_path, string file, int line)
        {
            TaskCompletionSource<IntPtr> taskCompletionSource = new TaskCompletionSource<IntPtr>(context);
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

            TaskCompletionSource<string> taskCompletionSourceStr = PendingCallbacks.Remove<string>(callbackId);

            if (level != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                taskCompletionSource.SetException(AriesAskarException.FromSdkError(error));
                return;
            }
            taskCompletionSourceStr.SetResult(message);
        }
        private static readonly GetLogCallbackDelegate GetLogCompletedCallback = GetLogCompletedCallbackMethod;

        private static void GetEnableCompletedCallbackMethod(IntPtr context, int level)
        {
            TaskCompletionSource<IntPtr> taskCompletionSource = new TaskCompletionSource<IntPtr>(context);
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

            TaskCompletionSource<sbyte> taskCompletionSourceInt = PendingCallbacks.Remove<sbyte>(callbackId);

            if (level != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                taskCompletionSource.SetException(AriesAskarException.FromSdkError(error));
                return;
            }
            taskCompletionSourceInt.SetResult(Convert.ToSByte(level));
        }
        private static readonly GetEnableCallbackDelegate GetEnableCompletedCallback = GetEnableCompletedCallbackMethod;

        private static void GetFlushCompleteCallbackMethod(IntPtr context)
        {
            TaskCompletionSource<IntPtr> taskCompletionSource = new TaskCompletionSource<IntPtr>(context);
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

            TaskCompletionSource<bool> taskCompletionSourceStr =  PendingCallbacks.Remove<bool>(callbackId);

            taskCompletionSourceStr.SetResult(true);
        }
        private static readonly GetFlushCallbackDelegate GetFlushCompletedCallback = GetFlushCompleteCallbackMethod;

        public static Task ClearCustomLoggerAsync()
        {
            NativeMethods.askar_clear_custom_logger();
            return Task.CompletedTask;
        }
        public static async Task<int> SetDefaultLoggerAsync()
        {
            int errorCode = NativeMethods.askar_set_default_logger();

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                throw AriesAskarException.FromSdkError(error);
            }

            return errorCode;
        }
        public static async Task<int> SetMaxLogLevelAsync(int maxlevel)
        {
            int errorCode = NativeMethods.askar_set_max_log_level(
                maxlevel);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                throw AriesAskarException.FromSdkError(error);
            }

            return errorCode;
        }
    }
}
