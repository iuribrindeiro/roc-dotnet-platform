# roc-dotnet-platform
DotNet platform for the [Roc](https://www.roc-lang.org/) language


The goal here is to learn a bit about C ABI with .NET while also playing with a purely functional language like Roc.

Roc has this amazign concept of [Platform](https://www.roc-lang.org/platforms), which allow us to choose 
where we wanna run roc on.

I chose to run it over .NET since I have most experience with it and would be really nice to play with 
exporting native code from C# code. I'm using [DNNE](https://github.com/AaronRobinsonMSFT/DNNE?tab=readme-ov-file) 
to do so.

If I'm able to make this work, I'll probably open a PR with it to use as a .NET platform 
[example for Roc](https://github.com/roc-lang/examples/tree/main/examples)
