using System;
using DotNetRocPlatform;

internal class Program
{
    private static unsafe void Main(string[] _)
    {
        Console.WriteLine("Hello from .NET!");

        Platform.MainFromRoc(out var rocStr);

        Console.WriteLine(Platform.RocStrRead(rocStr));
    }
}
