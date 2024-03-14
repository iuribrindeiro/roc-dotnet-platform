app "dotnetapp"
    packages { platform: "./platform/roc_platform.roc" }
    imports []
    provides [main] to platform

main = "hi from roc from C#"
