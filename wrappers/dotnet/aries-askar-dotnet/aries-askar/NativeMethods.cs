using System;
using System.Runtime.InteropServices;
using static indy_vdr_dotnet.models.Structures;

namespace aries_askar_dotnet.aries_askar
{
    internal static class NativeMethods
    {
        #region Error
        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern string askar_get_current_error(ref string errorJsonP);
        #endregion

        #region Key
        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_generate(FfiStr alg, byte ephemeral, ref uint localKeyHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_from_seed(FfiStr alg, ByteBuffer seed, FfiStr method, ref uint localKeyHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_from_jwk(ByteBuffer jwk, ref uint localKeyHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_from_public_bytes(FfiStr alg, ByteBuffer publicBytes, ref uint localKeyHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_get_public_bytes(uint localKeyHandle, ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_from_secret_bytes(FfiStr alg, ByteBuffer secret, ref uint localKeyHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_get_secret_bytes(uint localKeyHandle, ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_convert(uint inputHandle, FfiStr alg, ref uint outputHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_from_key_exchange(FfiStr alg, uint skHandle, uint pkHandle, ref uint localKeyHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_free(uint localKeyHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_get_algorithm(uint localKeyHandle, string alg);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_get_ephemeral(uint localKeyHandle, ref byte ephemeral);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_get_jwk_public(uint inputHandle, FfiStr alg, ref string jwk);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_get_jwk_secret(uint localKeyHandle, ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_get_jwk_thumbprint(uint localKeyHandle, FfiStr alg, ref string thumbprint);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_aead_random_nonce(uint localKeyHandle, ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_aead_get_params(uint localKeyHandle, ref AeadParams aeadparams);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_aead_get_padding(uint localKeyHandle, long msgLen, ref uint padding);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_aead_encrypt(uint localKeyHandle, ByteBuffer message, ByteBuffer nonce, ByteBuffer aad, ref EncryptedBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_aead_decrypt(uint localKeyHandle, ByteBuffer ciphertext, ByteBuffer nonce, ByteBuffer tag, ByteBuffer aad, ref SecretBuffer aead);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_sign_message(uint localKeyHandle, ByteBuffer message, FfiStr sigType, ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_verify_signature(uint localKeyHandle, ByteBuffer message, ByteBuffer signature, FfiStr sigType, ref byte verify);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_wrap_key(uint localKeyHandle, uint otherLocalKeyHandle, ByteBuffer nonce, ref EncryptedBuffer wrappedKey);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_unwrap_key(uint localKeyHandle, FfiStr alg, ByteBuffer ciphertext, ByteBuffer nonce, ByteBuffer tag, ref uint handle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_crypto_box_random_nonce(ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_crypto_box(uint recipKeyHandle, uint senderKeyHandle, ByteBuffer message, ByteBuffer nonce, ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_crypto_box_open(uint recipKeyHandle, uint senderKeyHandle, ByteBuffer message, ByteBuffer nonce, ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_crypto_box_seal(uint localKeyHandle, ByteBuffer message, ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_crypto_box_seal_open(uint localKeyHandle, ByteBuffer ciphertext, ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_derive_ecdh_es(FfiStr alg, uint ephemeralKeyHandle, uint recipKeyHandle, ByteBuffer algId, ByteBuffer apu, ByteBuffer apv, byte receive, ref uint localKeyHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_derive_ecdh_1pu(FfiStr alg, uint ephemeralKeyHandle, uint senderKeyHandle, uint recipKeyHandle, ByteBuffer algId, ByteBuffer apu, ByteBuffer apv, ByteBuffer ccTag, byte receive, ref uint localKeyHandle);
        #endregion

        #region Log
        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        // Option<EnableCallback, Option<FlushCallback>
        internal static extern int askar_set_custom_logger(IntPtr context, GetLogCallbackDelegate log, GetEnableCallbackDelegate enable, GetFlushCallbackDelegate flush, int max_level);
        internal delegate void GetLogCallbackDelegate(IntPtr context, int level, string target, string message, string module_path, string file, int line);
        internal delegate void GetEnableCallbackDelegate(IntPtr context, int level);
        internal delegate void GetFlushCallbackDelegate(IntPtr context);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_clear_custom_logger();

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_set_default_logger();

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_set_max_log_level(int max_level);
        #endregion

        #region Mod
        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern string askar_version();
        #endregion

        #region ResultList
        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_entry_list_count(uint entryListHandle, ref int count);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_entry_list_get_category(uint entryListHandle, int index, string category);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_entry_list_get_name(uint entryListHandle, int index, string name);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_entry_list_get_value(uint entryListHandle, int index, ref SecretBuffer value);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_entry_list_get_tags(uint entryListHandle, int index, string tags);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_entry_list_free(uint entryListHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_entry_list_count(uint keyEntryListHandle, ref int count);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_entry_list_free(uint keyEntryListHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_entry_list_get_algorithm(uint keyEntryListHandle, int index, string alg);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_entry_list_get_name(uint keyEntryListHandle, int index, string name);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_entry_list_get_metadata(uint keyEntryListHandle, int index, string metadata);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_entry_list_get_tags(uint keyEntryListHandle, int index, string tags);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_entry_list_load_local(uint keyEntryListHandle, int index, ref uint outLocalKeyHandle);
        #endregion

        #region Secret
        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_buffer_free(SecretBuffer buffer);
        #endregion

        #region Store
        internal delegate void NoReturnValueStoreCompletedDelegate(long callback_id, int err);
        internal delegate void GetStoreByteCompletedDelegate(long callback_id, int err, byte remove);
        internal delegate void GetStoreStringCompletedDelegate(long callback_id, int err, string result_p);
        internal delegate void GetStoreLongCompletedDelegate(long callback_id, int err, long count);
        internal delegate void GetStoreHandleCompletedDelegate(long callback_id, int err, uint handle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_store_generate_raw_key(ByteBuffer seed, ref string output);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_store_provision(FfiStr spec_uri, FfiStr key_method, FfiStr pass_key, FfiStr profile, byte recreate, GetStoreHandleCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_store_open(FfiStr spec_uri, FfiStr key_method, FfiStr pass_key, FfiStr profile, GetStoreHandleCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_store_remove(FfiStr spec_uri, GetStoreByteCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_store_create_profile(uint storeHandle, FfiStr profile, GetStoreStringCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_store_get_profile_name(uint storeHandle, GetStoreStringCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_store_remove_profile(uint storeHandle, FfiStr profile, GetStoreByteCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_store_rekey(uint storeHandle, FfiStr key_method, FfiStr pass_key, NoReturnValueStoreCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_store_close(uint storeHandle, NoReturnValueStoreCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_scan_start(uint storeHandle, FfiStr profile, FfiStr category, FfiStr tag_filter, long offset, long limit, GetStoreHandleCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_scan_next(uint scanHandle, GetStoreHandleCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_scan_free(uint scanHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_start(uint storeHandle, FfiStr profile, byte as_transaction, GetStoreHandleCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_count(uint storeHandle, FfiStr category, FfiStr tag_filter, GetStoreLongCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_fetch(uint storeHandle, FfiStr category, FfiStr name, byte for_update, GetStoreHandleCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_fetch_all(uint storeHandle, FfiStr category, FfiStr tag_filter, long limit, byte for_update, GetStoreHandleCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_remove_all(uint storeHandle, FfiStr category, FfiStr tag_filter, GetStoreLongCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_update(uint sesstionHandle, byte operation, FfiStr category, FfiStr name, ByteBuffer value, FfiStr tags, long expiry_ms, NoReturnValueStoreCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_insert_key(uint storeHandle, uint localKeyHandle, FfiStr tag_filter, FfiStr metadata, FfiStr tags, long expiry_ms, NoReturnValueStoreCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_fetch_key(uint storeHandle, FfiStr name, byte for_update, GetStoreHandleCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_fetch_all_keys(uint sessionHandle, FfiStr alg, FfiStr thumbprint, FfiStr tag_filter, long limit, byte for_update, GetStoreHandleCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_update_key(uint sessionHandle, FfiStr name, FfiStr metadata, FfiStr tags, long expiry_ms, NoReturnValueStoreCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_remove_key(uint sessionHandle, FfiStr name, NoReturnValueStoreCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_close(uint sessionHandle, byte commit, NoReturnValueStoreCompletedDelegate cb, long cb_id);
        #endregion
    }
}