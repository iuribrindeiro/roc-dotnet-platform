using System;
using DotNetRocPlatform;

internal unsafe class Program
{
    private static unsafe void Main(string[] _)
    {
        Console.WriteLine("Hello from .NET!");

        Platform.MainFromRoc(out var rocStr);

        PrintRocStr(rocStr);
    }

    private static void PrintRocStr(RocStr rocStr)
    {
        using var disposableString = Platform.RocStrRead(&rocStr);

        Console.WriteLine(disposableString);
    }
}
