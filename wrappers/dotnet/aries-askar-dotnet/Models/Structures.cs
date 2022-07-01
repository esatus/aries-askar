using System;
using System.Runtime.InteropServices;
using System.Text;

namespace indy_vdr_dotnet.models
{
    public static class Structures
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct FfiStr
        {
            public IntPtr data;

            public static FfiStr Create(string arg)
            {
                FfiStr FfiString = new();
                FfiString.data = new IntPtr();
                if (arg != null)
                {
                    FfiString.data = Marshal.StringToCoTaskMemUTF8(arg);
                }
                return FfiString;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ByteBuffer
        {
            public long len;
            public byte* value;

            public static ByteBuffer Create(string json)
            {
                ByteBuffer buffer = new();
                if (json != null)
                {
                    UTF8Encoding decoder = new(true, true);
                    byte[] bytes = new byte[json.Length];
                    decoder.GetBytes(json, 0, json.Length, bytes, 0);
                    buffer.len = json.Length;
                    fixed (byte* bytebuffer_p = &bytes[0])
                    {
                        buffer.value = bytebuffer_p;
                    }
                }
                else
                {
                    buffer.len = 0;
                    buffer.value = null;
                }
                return buffer;
            }

            public static ByteBuffer Create(byte[] bytes)
            {
                ByteBuffer buffer = new();
                buffer.len = bytes.Length;

                if(buffer.len > 0)
                {
                    fixed (byte* bytebuffer_p = &bytes[0])
                    {
                        buffer.value = bytebuffer_p;
                    }
                }
                else
                {
                    buffer.value = null;
                }

                return buffer;
            }
        }

        public unsafe static byte[] Decode(this ByteBuffer buffer)
        {
            byte[] managedArray = new byte[buffer.len];

            if(buffer.len > 0)
            {
                Marshal.Copy(new IntPtr(buffer.value), managedArray, 0, (int)buffer.len);
            }

            return managedArray;
        }
        public unsafe static string DecodeToString(this ByteBuffer buffer)
        {
            return Marshal.PtrToStringUTF8(new IntPtr(buffer.value), (int)buffer.len);
        }

        public unsafe static (byte[], byte[], byte[]) Decode(this EncryptedBuffer encryptedBuffer)
        {
            byte[] source = encryptedBuffer.buffer.Decode();
            byte[] valueBytes = null;
            byte[] tagBytes = null;
            byte[] nonceBytes = null;

            if (encryptedBuffer.buffer.len > 0)
            {
                if (encryptedBuffer.buffer.len > encryptedBuffer.tag_pos && encryptedBuffer.tag_pos >= 0)
                {
                    valueBytes = new byte[encryptedBuffer.tag_pos];
                    Array.Copy(source, 0, valueBytes, 0, encryptedBuffer.tag_pos);
                    //tag and nonce exist
                    if (encryptedBuffer.buffer.len > encryptedBuffer.nonce_pos && encryptedBuffer.nonce_pos >= 0)
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
                    if (encryptedBuffer.buffer.len > encryptedBuffer.nonce_pos && encryptedBuffer.nonce_pos >= 0)
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
        /**
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct FfiEntryList
        {
            public FfiEntry category;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct FfiEntry
        {
            public FfiStr category;
            public FfiStr name;
            public ByteBuffer value;
            public byte* tags;
        }**/
    }
}
