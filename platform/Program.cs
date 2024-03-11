using System.Runtime.InteropServices;

namespace DotNetRocPlatform;

public static unsafe class Platform
{
    [UnmanagedCallersOnly(EntryPoint = "print")]
    [DNNE.C99DeclCode("#include <roc.h>")]
    public static void Print([DNNE.C99Type("const struct RocStr*")] RocStr* str)
    {
        var is64Bit = IntPtr.Size == 8;

        Console.WriteLine(RocStrRead(str, is64Bit));
    }

    public static string RocStrRead(RocStr* rocStr, bool is64Bit)
    {
        int len;
        byte* ptr;
        if (rocStr->capacity < 0)
        {
            // Small string
            ptr = (byte*)&rocStr;

            int byteLen = is64Bit ? 24 : 12;

            string shortStr = new((sbyte*)ptr, 0, byteLen - 1, System.Text.Encoding.UTF8);
            len = shortStr[byteLen - 1] ^ 128;
            return shortStr[..len];
        }

        // Remove the bit for seamless string
        len = (rocStr->len << 1) >> 1;
        ptr = (byte*)rocStr->bytes;
        return new string((sbyte*)ptr, 0, len, System.Text.Encoding.UTF8);
    }

    [UnmanagedCallersOnly(EntryPoint = "throw_error")]
    public static void ThrowError([DNNE.C99Type("char*")] char* msg) =>
        throw new Exception(new string(msg));
}

public struct RocStr
{
    public IntPtr bytes;
    public int len;
    public int capacity;
}
