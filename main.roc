app "hello"
    packages { platform: "https://github.com/roc-lang/basic-cli/releases/download/0.8.1/x8URkvfyi9I0QhmVG98roKBUs_AZRkLFwFJVJ3942YA.tar.br" }
    imports [platform.Stdout]
    provides [main] to platform

main =
    Stdout.line "I'm a Roc application!"
