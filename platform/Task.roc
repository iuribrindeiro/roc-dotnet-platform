interface Task
    exposes [Task, await, err]
    imports [InternalTask, Effect]

Task ok err : InternalTask.Task ok err

err : a -> Task * a
err = \a -> InternalTask.err a

await : Task a b, (a -> Task c b) -> Task c b
await = \task, transform ->
    effect = Effect.after
        (InternalTask.toEffect task)
        \result ->
            when result is
                Ok a -> transform a |> InternalTask.toEffect
                Err b -> err b |> InternalTask.toEffect

    InternalTask.fromEffect effect
