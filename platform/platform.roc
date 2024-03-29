platform "dotnetplatform"
    requires {} { main : Task {} I32 }
    exposes [Console]
    packages {}
    imports [Task.{ Task }]
    provides [mainForHost]

mainForHost : Task {} I32 as Fx
mainForHost = main
