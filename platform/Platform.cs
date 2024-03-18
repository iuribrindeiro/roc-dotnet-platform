using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotNetRocPlatform;

public static unsafe partial class Platform
{
    public static DisposableString RocStrRead(RocStr* rocStr) =>
        Encoding.UTF8.GetString(rocStr->bytes, (int)rocStr->len.ToUInt32());

    [LibraryImport("interop", EntryPoint = "roc__mainForHost_1_exposed_generic")]
    internal static partial void MainFromRoc(out RocStr rocStr);
}

//Just to make the use of strings read from Roc easy to free from memory
public readonly unsafe record struct DisposableString : IDisposable
{
    public required string Value { get; init; }

    public static implicit operator DisposableString(Span<char> value) =>
        new() { Value = value.ToString() };

    public static implicit operator DisposableString(string value) => new() { Value = value };

    public void Dispose() => Marshal.FreeHGlobal(Marshal.StringToHGlobalAnsi(Value));

    public override string ToString() => Value;

    public static implicit operator string(DisposableString disposableString) =>
        disposableString.Value;
}

public unsafe struct RocStr
{
    public byte* bytes;
    public UIntPtr len;
    public UIntPtr capacity;
}
