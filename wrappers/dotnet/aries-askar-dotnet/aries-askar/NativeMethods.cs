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
        internal static extern int askar_key_get_secret_bytes(FfiStr alg, ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_convert(uint inputHandle, FfiStr alg, ref uint outputHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_from_key_exchange(FfiStr alg, uint skHandle, uint pkHandle, ref uint localKeyHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_free(uint localKeyHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_get_algorithm(uint localKeyHandle, ref string alg);

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
        internal static extern int askar_key_sign_message(uint localKeyHandle, ByteBuffer message, FfiStr sigType , ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_verify_signature(uint localKeyHandle, ByteBuffer message, ByteBuffer signature, FfiStr sigType , ref byte verify);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_wrap_key(uint localKeyHandle, uint otherLocalKeyHandle, ByteBuffer nonce, ref EncryptedBuffer wrappedKey);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_unwrap_key(uint localKeyHandle, FfiStr alg, ByteBuffer ciphertext, ByteBuffer nonce, ByteBuffer tag, ref uint handle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_crypto_box_random_nonce(ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_crypto_box(uint recipKeyHandle, uint senderKeyHandle, ByteBuffer message, ByteBuffer nonce, ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_crypto_box_open(uint recipKeyHandle, uint senderKeyHandle, ByteBuffer message, ByteBuffer nonce, SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_crypto_box_seal(uint localKeyHandle, ByteBuffer message, ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_crypto_box_seal_open(uint localKeyHandle, ByteBuffer ciphertext, ref SecretBuffer secret);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_derive_ecdh_es(FfiStr alg, uint ephemeralKeyHandle, uint recipKeyHandle, ByteBuffer algId, ByteBuffer apu, ByteBuffer apv, byte receive, ref uint localKeyHandle);

        [DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int askar_key_derive_ecdh_1pu(FfiStr alg, uint ephemeralKeyHandle, uint senderKeyHandle, uint recipKeyHandle, ByteBuffer algId, ByteBuffer apu, ByteBuffer apv, ByteBuffer ccTag, byte receive, ref uint localKeyHandle);
        #endregion
    }
}