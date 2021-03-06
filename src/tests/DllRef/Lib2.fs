module Fable.Tests.DllRef.Lib2

open Fable.Core
open Fable.Core.JsInterop

type IBar =
    abstract generator: unit -> string

#if FABLE_COMPILER
/// Including same JS file from different F# sources works
let foo: string = importMember "./js1/lib.js"

/// Classes from included JS files work
[<Import("Bar","./js2/lib.js")>]
type Bar(i: int, s: string) =
    member __.generator(): string = jsNative

/// Default imports work
let bar: string = importDefault "./js2/lib.js"

/// JSConstructor works
let BarCons: JsConstructor<int*string,IBar> = import "Bar" "./js2/lib.js"
#else
let foo = "foo"

type Bar(i: int, s: string) =
    member __.generator() =
        String.replicate i s
let bar = "bar"

let Bar(i, s) = Bar(i, s)
#endif
