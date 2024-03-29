using System.Runtime.InteropServices;
using System.Text;

public static unsafe class Exports
{
    [UnmanagedCallersOnly(EntryPoint = "roc_alloc")]
    public static IntPtr RocAlloc(UIntPtr size, UInt32 alignment) =>
        Marshal.AllocHGlobal((int)size);

    [UnmanagedCallersOnly(EntryPoint = "roc_realloc")]
    public static IntPtr RocRealloc(
        IntPtr ptr,
        UIntPtr newSize,
        UIntPtr oldSize,
        UInt32 alignment
    ) => Marshal.ReAllocHGlobal(ptr, (nint)newSize);

    [UnmanagedCallersOnly(EntryPoint = "roc_dealloc")]
    public static void RocDealloc(IntPtr ptr, UInt32 aligment) => Marshal.FreeHGlobal(ptr);

    [UnmanagedCallersOnly(EntryPoint = "init")]
    public static void LoadLibrary() { }

    [UnmanagedCallersOnly(EntryPoint = "roc_fx_consoleWriteLine")]
    [DNNE.C99DeclCode("struct RocStr { char* bytes; size_t len; size_t capacity; };")]
    public static void ConsoleWriteLineFx([DNNE.C99Type("struct RocStr")] RocStr rocStr) =>
        Console.WriteLine(rocStr.ToString());
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct RocStr
{
    public byte* Bytes;
    public UIntPtr Len;
    public UIntPtr Capacity;

    public override string ToString() => Encoding.UTF8.GetString(Bytes, (int)Len.ToUInt32());

    public static implicit operator string(RocStr rocStr) => rocStr.ToString();
}
