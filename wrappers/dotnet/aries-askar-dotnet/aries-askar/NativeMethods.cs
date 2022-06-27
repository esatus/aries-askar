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
        internal static extern int askar_key_get_public_bytes(IntPtr localKeyHandle, ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_get_secret_bytes(IntPtr localKeyHandle, ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_get_algorithm(IntPtr localKeyHandle, ref string alg);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_get_ephemeral(IntPtr localKeyHandle, ref byte ephemeral);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_get_jwk_public(IntPtr inputHandle, FfiStr alg, ref string jwk);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_get_jwk_secret(IntPtr localKeyHandle, ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_get_jwk_thumbprint(IntPtr localKeyHandle, FfiStr alg, ref string thumbprint);
        #endregion

        #region aead
        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_aead_random_nonce(IntPtr localKeyHandle, ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_aead_get_params(IntPtr localKeyHandle, ref AeadParams aeadparams);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_aead_get_padding(IntPtr localKeyHandle, long msgLen, ref int padding);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_aead_encrypt(IntPtr localKeyHandle, ByteBuffer message, ByteBuffer nonce, ByteBuffer aad, ref EncryptedBuffer encrypted);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_aead_decrypt(IntPtr localKeyHandle, ByteBuffer ciphertext, ByteBuffer nonce, ByteBuffer tag, ByteBuffer aad, ref SecretBuffer aead);
        #endregion

        #region Crypto
        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_crypto_box_random_nonce(ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_crypto_box(IntPtr recipKeyHandle, IntPtr senderKeyHandle, ByteBuffer message, ByteBuffer nonce, ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_crypto_box_open(IntPtr recipKeyHandle, IntPtr senderKeyHandle, ByteBuffer message, ByteBuffer nonce, SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_crypto_box_seal(IntPtr localKeyHandle, ByteBuffer message, ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_crypto_box_seal_open(IntPtr localKeyHandle, ByteBuffer ciphertext, ref SecretBuffer secret);
        #endregion

        #region Util
        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_convert(IntPtr inputHandle, FfiStr alg, ref IntPtr outputHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_free(IntPtr localKeyHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_sign_message(IntPtr localKeyHandle, ByteBuffer message, FfiStr sigType , ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_verify_signature(IntPtr localKeyHandle, ByteBuffer message, ByteBuffer signature, FfiStr sigType , ref byte verify);

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
    }
}