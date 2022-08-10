using System;
using System.Runtime.InteropServices;
using System.Text;

namespace aries_askar_dotnet.Models
{
    public static class Structures
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct FfiStr
        {
            public IntPtr data;

            public static FfiStr Create(string arg)
            {
                FfiStr FfiString = new FfiStr()
                {
                    data = new IntPtr()
                };
                if (arg != null)
                {
                    FfiString.data = Marshal.StringToCoTaskMemAnsi(arg);
                }
                return FfiString;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ByteBuffer
        {
            public long len;
            public IntPtr value;

            public static ByteBuffer Create(string json)
            {
                ByteBuffer buffer = new ByteBuffer();
                if (!string.IsNullOrEmpty(json))
                {
                    UTF8Encoding decoder = new UTF8Encoding(true, true);
                    byte[] bytes = new byte[json.Length];
                    _ = decoder.GetBytes(json, 0, json.Length, bytes, 0);
                    buffer.len = json.Length;
                    fixed (byte* bytebuffer_p = &bytes[0])
                    {
                        buffer.value = new IntPtr(bytebuffer_p);
                    }
                }
                else
                {
                    buffer.len = 0;
                    buffer.value = new IntPtr();
                }
                return buffer;
            }

            public static ByteBuffer Create(byte[] bytes)
            {
                ByteBuffer buffer = new ByteBuffer();
                buffer.len = bytes != null ? bytes.Length : 0;

                if (buffer.len > 0 && bytes != null)
                {
                    fixed (byte* bytebuffer_p = &bytes[0])
                    {
                        buffer.value = new IntPtr(bytebuffer_p);
                    }
                }
                else
                {
                    buffer.value = new IntPtr();
                }

                return buffer;
            }
        }

        public static unsafe string DecodeToString(this ByteBuffer buffer)
        {
            switch (buffer.len)
            {
                case 0: return "";
                default:
                    char[] charArray = new char[buffer.len];
                    UTF8Encoding utf8Decoder = new UTF8Encoding(true, true);

                    fixed (char* char_ptr = &charArray[0])
                    {
                        _ = utf8Decoder.GetChars((byte*)buffer.value, (int)buffer.len, char_ptr, (int)buffer.len);
                    }
                    return new string(charArray);
            }
        }

        public static unsafe byte[] Decode(this ByteBuffer buffer)
        {
            byte[] managedArray = new byte[buffer.len];

            if (buffer.len > 0)
            {
                Marshal.Copy(buffer.value, managedArray, 0, (int)buffer.len);
            }

            return managedArray;
        }

        public static unsafe (byte[], byte[], byte[]) Decode(this EncryptedBuffer encryptedBuffer)
        {
            byte[] source = encryptedBuffer.buffer.Decode();
            byte[] valueBytes = null;
            byte[] tagBytes = null;
            byte[] nonceBytes = null;

            if (encryptedBuffer.buffer.len > 0)
            {
                if (encryptedBuffer.buffer.len > encryptedBuffer.tag_pos && encryptedBuffer.tag_pos > 0)
                {
                    valueBytes = new byte[encryptedBuffer.tag_pos];
                    Array.Copy(source, 0, valueBytes, 0, encryptedBuffer.tag_pos);
                    //tag and nonce exist
                    if (encryptedBuffer.buffer.len > encryptedBuffer.nonce_pos && encryptedBuffer.nonce_pos > 0)
                    {
                        tagBytes = new byte[encryptedBuffer.nonce_pos - encryptedBuffer.tag_pos];
                        Array.Copy(source, encryptedBuffer.tag_pos, tagBytes, 0, encryptedBuffer.nonce_pos - encryptedBuffer.tag_pos);

                        nonceBytes = new byte[encryptedBuffer.buffer.len - encryptedBuffer.nonce_pos];
                        Array.Copy(source, encryptedBuffer.nonce_pos, nonceBytes, 0, encryptedBuffer.buffer.len - encryptedBuffer.nonce_pos);
                    }
                    //only tag exists
                    else
                    {
                        tagBytes = new byte[encryptedBuffer.buffer.len - encryptedBuffer.tag_pos];
                        Array.Copy(source, encryptedBuffer.tag_pos, tagBytes, 0, encryptedBuffer.buffer.len - encryptedBuffer.tag_pos);
                    }
                }
                else
                {
                    //only nonce exists
                    if (encryptedBuffer.buffer.len > encryptedBuffer.nonce_pos && encryptedBuffer.nonce_pos > 0)
                    {
                        valueBytes = new byte[encryptedBuffer.nonce_pos];
                        Array.Copy(source, 0, valueBytes, 0, encryptedBuffer.nonce_pos);

                        nonceBytes = new byte[encryptedBuffer.buffer.len - encryptedBuffer.nonce_pos];
                        Array.Copy(source, encryptedBuffer.nonce_pos, nonceBytes, 0, encryptedBuffer.buffer.len - encryptedBuffer.nonce_pos);
                    }
                    //neither tag nor nonce exist
                    else
                    {
                        valueBytes = source;
                    }
                }
            }

            return (valueBytes, tagBytes, nonceBytes);
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct EncryptedBuffer
        {
            public ByteBuffer buffer;
            public long tag_pos;
            public long nonce_pos;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct AeadParams
        {
            public uint nonce_length;
            public uint tag_length;
        }
    }
}