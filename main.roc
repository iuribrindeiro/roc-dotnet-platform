app "dotnetapp"
    packages { pf: "./platform/platform.roc" }
    imports [pf.Console]
    provides [main] to pf

main = 
    Console.writeLine "Hi from roc! (in a .NET platform) 🔥🦅🔥" 
