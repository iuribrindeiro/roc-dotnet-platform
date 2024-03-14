using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotNetRocPlatform;

public static partial class Platform
{
    public static unsafe string RocStrRead(RocStr rocStr)
    {
        //not sure why, but when converting from UIntPtr to long, it becomes negative
        if ((long)rocStr.capacity < 0)
        {
            // Small string
            int byteLen = IntPtr.Size == 8 ? 24 : 12;
            byte[] bytes = new byte[byteLen];
            //getting the actual reference to the rocStr, converting it to a IntPtr
            //and then copying its byte array content to `bytes` seems to work,
            //but using the actual instance .bytes doesnt.
            //I still need to understand what is the .bytes and why is it differnet from (IntPtr)(&rocStr)
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
