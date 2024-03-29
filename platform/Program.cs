using System.Runtime.InteropServices;
using System.Text;
using static Platform;

LoadDotNetLibrary();

var size = MainFromRocSize();

var buffer = Marshal.AllocHGlobal(size);

MainFromRoc(buffer);

var output = CallTheClosure(buffer);

Console.WriteLine(output);

return output;

static int CallTheClosure(IntPtr closureDataPtr)
{
    // Main always returns an i32. just allocate for that.
    CallFx(IntPtr.Zero, closureDataPtr, out var outResult);

    Console.WriteLine(outResult.Tag);
    Console.WriteLine(outResult.Payload.Ok);
    Console.WriteLine(outResult.Payload.Err);

    return outResult.Tag is RocResultTag.RocOk ? 0 : outResult.Payload.Err;
}

public static partial class Platform
{
    [LibraryImport("interop", EntryPoint = "roc__mainForHost_1_exposed_generic")]
    internal static partial void MainFromRoc(IntPtr buffer);

    [LibraryImport("interop", EntryPoint = "roc__mainForHost_1_exposed_size")]
    internal static partial int MainFromRocSize();

    [LibraryImport("interop", EntryPoint = "roc__mainForHost_0_caller")]
    internal static partial void CallFx(IntPtr flags, IntPtr closureData, out RocResult data);

    [LibraryImport("ExportsNE", EntryPoint = "init")]
    internal static partial void LoadDotNetLibrary();
}

[StructLayout(LayoutKind.Explicit)]
public struct RocResultPayload
{
    [FieldOffset(0)]
    public int Ok;

    [FieldOffset(0)]
    public int Err;
}

public enum RocResultTag : byte
{
    RocErr = 0,
    RocOk = 1,
}

[StructLayout(LayoutKind.Sequential)]
public struct RocResult
{
    public RocResultPayload Payload;
    public RocResultTag Tag;
}

public unsafe struct RocStr
{
    public byte* Bytes;
    public UIntPtr Len;
    public UIntPtr Capacity;

    public override string ToString() => Encoding.UTF8.GetString(Bytes, (int)Len.ToUInt32());

    public static implicit operator string(RocStr rocStr) => rocStr.ToString();
}
