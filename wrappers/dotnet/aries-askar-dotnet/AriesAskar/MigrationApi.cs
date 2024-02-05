using aries_askar_dotnet.utils;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using static aries_askar_dotnet.Models.Structures;
using System.Threading.Tasks;
using static aries_askar_dotnet.AriesAskar.NativeMethods;

namespace aries_askar_dotnet.AriesAskar
{
    public static class MigrationApi
    {
        /// <param name="specuri">Specify URI of database to be migrated.</param>
        /// /// <param name="walletname">Specify name of wallet to be migrated for DatabasePerWallet.</param>
        /// /// <param name="walletkey">Specify key corresponding to the given name of the wallet to be migrated for database per wallet (dbpw) migration strategy.</param>
        /// /// <param name="kdflevel">Handle of the pool object.</param>
        public static async Task<bool> MigrateIndySdkAsync(
                    string specuri,
                    string walletname,
                    string walletkey,
                    string kdflevel)
            {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.askar_migrate_indy_sdk(
                FfiStr.Create(specuri),
                FfiStr.Create(walletname),
                FfiStr.Create(walletkey),
                FfiStr.Create(kdflevel),
                MigrationCompletedCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                throw AriesAskarException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }

        private static void MigrationCompletedCallbackMethod(long callbackId, int errorCode)
        {
            TaskCompletionSource<bool> taskCompletionSource = PendingCallbacks.Remove<bool>(callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                taskCompletionSource.SetException(AriesAskarException.FromSdkError(error));
                return;
            }
            taskCompletionSource.SetResult(true);
        }
        private static readonly MigrationCompletedDelegate MigrationCompletedCallback = MigrationCompletedCallbackMethod;
    }
}
