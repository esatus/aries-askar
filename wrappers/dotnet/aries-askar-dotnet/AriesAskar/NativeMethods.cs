using System;
using System.Runtime.InteropServices;
using static indy_vdr_dotnet.models.Structures;

namespace aries_askar_dotnet.AriesAskar
{
    /// <summary>
    /// Api calls to the native code.
    /// </summary>
    internal static class NativeMethods
    {
        #region Error
        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern string askar_get_current_error(ref string errorJsonP);
        #endregion

        #region Key
        #region Create
        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_generate(FfiStr alg, byte ephemeral, ref IntPtr localKeyHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_from_seed(FfiStr alg, ByteBuffer seed, FfiStr method, ref IntPtr localKeyHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_from_jwk(ByteBuffer jwk, ref IntPtr localKeyHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_from_public_bytes(FfiStr alg, ByteBuffer publicBytes, ref IntPtr localKeyHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_from_secret_bytes(FfiStr alg, ByteBuffer secret, ref IntPtr localKeyHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_from_key_exchange(FfiStr alg, IntPtr skHandle, IntPtr pkHandle, ref IntPtr localKeyHandle);
        #endregion

        #region Get
        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_get_public_bytes(IntPtr localKeyHandle, ref ByteBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_get_secret_bytes(IntPtr localKeyHandle, ref ByteBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_get_algorithm(IntPtr localKeyHandle, ref string alg);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_get_ephemeral(IntPtr localKeyHandle, ref byte ephemeral);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_get_jwk_public(IntPtr inputHandle, FfiStr alg, ref string jwk);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_get_jwk_secret(IntPtr localKeyHandle, ref ByteBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_get_jwk_thumbprint(IntPtr localKeyHandle, FfiStr alg, ref string thumbprint);
        #endregion

        #region aead
        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_aead_random_nonce(IntPtr localKeyHandle, ref ByteBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_aead_get_params(IntPtr localKeyHandle, ref AeadParams aeadparams);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_aead_get_padding(IntPtr localKeyHandle, long msgLen, ref int padding);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_aead_encrypt(IntPtr localKeyHandle, ByteBuffer message, ByteBuffer nonce, ByteBuffer aad, ref EncryptedBuffer encrypted);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_aead_decrypt(IntPtr localKeyHandle, ByteBuffer ciphertext, ByteBuffer nonce, ByteBuffer tag, ByteBuffer aad, ref ByteBuffer aead);
        #endregion

        #region Crypto
        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_crypto_box_random_nonce(ref ByteBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_crypto_box(IntPtr recipKeyHandle, IntPtr senderKeyHandle, ByteBuffer message, ByteBuffer nonce, ref ByteBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_crypto_box_open(IntPtr recipKeyHandle, IntPtr senderKeyHandle, ByteBuffer message, ByteBuffer nonce, ref ByteBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_crypto_box_seal(IntPtr localKeyHandle, ByteBuffer message, ref ByteBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_crypto_box_seal_open(IntPtr localKeyHandle, ByteBuffer ciphertext, ref ByteBuffer secret);
        #endregion

        #region Utils
        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_convert(IntPtr inputHandle, FfiStr alg, ref IntPtr outputHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern void askar_key_free(IntPtr localKeyHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_sign_message(IntPtr localKeyHandle, ByteBuffer message, FfiStr sigType, ref ByteBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_verify_signature(IntPtr localKeyHandle, ByteBuffer message, ByteBuffer signature, FfiStr sigType, ref byte verify);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_wrap_key(IntPtr localKeyHandle, IntPtr otherLocalKeyHandle, ByteBuffer nonce, ref EncryptedBuffer wrappedKey);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_unwrap_key(IntPtr localKeyHandle, FfiStr alg, ByteBuffer ciphertext, ByteBuffer nonce, ByteBuffer tag, ref IntPtr handle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_derive_ecdh_es(FfiStr alg, IntPtr ephemeralKeyHandle, IntPtr recipKeyHandle, ByteBuffer algId, ByteBuffer apu, ByteBuffer apv, byte receive, ref IntPtr localKeyHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_derive_ecdh_1pu(FfiStr alg, IntPtr ephemeralKeyHandle, IntPtr senderKeyHandle, IntPtr recipKeyHandle, ByteBuffer algId, ByteBuffer apu, ByteBuffer apv, ByteBuffer ccTag, byte receive, ref IntPtr localKeyHandle);
        #endregion
        #endregion

        #region Log
        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
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
        internal static extern int askar_entry_list_count(IntPtr entryListHandle, ref int count);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_entry_list_get_category(IntPtr entryListHandle, int index, ref string category);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_entry_list_get_name(IntPtr entryListHandle, int index, ref string name);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_entry_list_get_value(IntPtr entryListHandle, int index, ref ByteBuffer value);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_entry_list_get_tags(IntPtr entryListHandle, int index, ref string tags);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern void askar_entry_list_free(IntPtr entryListHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_entry_list_count(IntPtr keyEntryListHandle, ref int count);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern void askar_key_entry_list_free(IntPtr keyEntryListHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_entry_list_get_algorithm(IntPtr keyEntryListHandle, int index, ref string alg);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_entry_list_get_name(IntPtr keyEntryListHandle, int index, ref string name);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_entry_list_get_metadata(IntPtr keyEntryListHandle, int index, ref string metadata);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_entry_list_get_tags(IntPtr keyEntryListHandle, int index, ref string tags);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_entry_list_load_local(IntPtr keyEntryListHandle, int index, ref IntPtr outLocalKeyHandle);
        #endregion

        #region Secret
        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_buffer_free(ByteBuffer buffer);
        #endregion

        #region Store
        internal delegate void NoReturnValueStoreCompletedDelegate(long callback_id, int err);
        internal delegate void GetStoreByteCompletedDelegate(long callback_id, int err, byte remove);
        internal delegate void GetStoreStringCompletedDelegate(long callback_id, int err, string result_p);
        internal delegate void GetStoreLongCompletedDelegate(long callback_id, int err, long count);
        internal delegate void GetStoreHandleCompletedDelegate(long callback_id, int err, IntPtr handle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_store_generate_raw_key(ByteBuffer seed, ref string output);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_store_provision(FfiStr spec_uri, FfiStr key_method, FfiStr pass_key, FfiStr profile, byte recreate, GetStoreHandleCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_store_open(FfiStr spec_uri, FfiStr key_method, FfiStr pass_key, FfiStr profile, GetStoreHandleCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_store_remove(FfiStr spec_uri, GetStoreByteCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_store_create_profile(IntPtr storeHandle, FfiStr profile, GetStoreStringCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_store_get_profile_name(IntPtr storeHandle, GetStoreStringCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_store_remove_profile(IntPtr storeHandle, FfiStr profile, GetStoreByteCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_store_rekey(IntPtr storeHandle, FfiStr key_method, FfiStr pass_key, NoReturnValueStoreCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_store_close(IntPtr storeHandle, NoReturnValueStoreCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_scan_start(IntPtr storeHandle, FfiStr profile, FfiStr category, FfiStr tag_filter, long offset, long limit, GetStoreHandleCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_scan_next(IntPtr scanHandle, GetStoreHandleCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_scan_free(IntPtr scanHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_start(IntPtr storeHandle, FfiStr profile, byte as_transaction, GetStoreHandleCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_count(IntPtr storeHandle, FfiStr category, FfiStr tag_filter, GetStoreLongCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_fetch(IntPtr storeHandle, FfiStr category, FfiStr name, byte for_update, GetStoreHandleCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_fetch_all(IntPtr storeHandle, FfiStr category, FfiStr tag_filter, long limit, byte for_update, GetStoreHandleCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_remove_all(IntPtr storeHandle, FfiStr category, FfiStr tag_filter, GetStoreLongCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_update(IntPtr sesstionHandle, byte operation, FfiStr category, FfiStr name, ByteBuffer value, FfiStr tags, long expiry_ms, NoReturnValueStoreCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_insert_key(IntPtr storeHandle, IntPtr localKeyHandle, FfiStr tag_filter, FfiStr metadata, FfiStr tags, long expiry_ms, NoReturnValueStoreCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_fetch_key(IntPtr storeHandle, FfiStr name, byte for_update, GetStoreHandleCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_fetch_all_keys(IntPtr sessionHandle, FfiStr alg, FfiStr thumbprint, FfiStr tag_filter, long limit, byte for_update, GetStoreHandleCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_update_key(IntPtr sessionHandle, FfiStr name, FfiStr metadata, FfiStr tags, long expiry_ms, NoReturnValueStoreCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_remove_key(IntPtr sessionHandle, FfiStr name, NoReturnValueStoreCompletedDelegate cb, long cb_id);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_session_close(IntPtr sessionHandle, byte commit, NoReturnValueStoreCompletedDelegate cb, long cb_id);
        #endregion
    }
}