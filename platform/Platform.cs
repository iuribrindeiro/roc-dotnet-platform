using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotNetRocPlatform;

public static partial class Platform
{
    public static unsafe string RocStrRead(RocStr rocStr)
    {
        if ((long)rocStr.capacity < 0)
        {
            // Small string
            int byteLen = IntPtr.Size == 8 ? 24 : 12;
            byte[] bytes = new byte[byteLen];
            Marshal.Copy((IntPtr)(&rocStr), bytes, 0, byteLen);
            int lenSmall = bytes[byteLen - 1] ^ 128;
            return Encoding.ASCII.GetString(bytes, 0, lenSmall);
        }

        // Remove the bit for seamless string
        long len = (long)((rocStr.len << 1) >> 1);
        byte[] longStr = new byte[len];
        Marshal.Copy(rocStr.bytes, longStr, 0, (int)len);
        return Encoding.UTF8.GetString(longStr);
    }

    [LibraryImport("interop", EntryPoint = "roc__mainForHost_1_exposed_generic")]
    internal static partial void MainFromRoc(out RocStr rocStr);
}

public unsafe struct RocStr
{
    public IntPtr bytes;
    public UIntPtr len;
    public UIntPtr capacity;
}
