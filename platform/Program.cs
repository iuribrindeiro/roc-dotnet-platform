using System.Runtime.InteropServices;
using System.Text;
using static System.Console;
using static Platform;

WriteLine("Hello from .NET!");

MainFromRoc(out var rocStr);

var disposableString = RocStrRead(rocStr);

WriteLine(disposableString.ToString());

public static unsafe partial class Platform
{
    public static ReadOnlySpan<char> RocStrRead(RocStr rocStr) =>
        Encoding.UTF8.GetString(rocStr.Bytes, (int)rocStr.Len.ToUInt32());

    [LibraryImport("interop", EntryPoint = "roc__mainForHost_1_exposed_generic")]
    internal static partial void MainFromRoc(out RocStr rocStr);
}

public unsafe struct RocStr
{
    public byte* Bytes;
    public UIntPtr Len;
    public UIntPtr Capacity;
}
