interface Console
    exposes [writeLine]
    imports [Effect, Task.{ Task }, InternalTask]


writeLine : Str -> Task {} I32
writeLine = \str -> 
    Effect.consoleWriteLine str
    |> Effect.map (\_ -> Ok {})
    |> InternalTask.fromEffect
