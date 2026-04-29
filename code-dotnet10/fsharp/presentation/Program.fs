// F#
open System
open FSharpPlus

let toUpper (s: string) : string = s.ToUpper()

let stringToOption (s: string) : string option =
    if String.IsNullOrWhiteSpace s then None else Some s

let nonEmptyStringToUpper (s: string) : string option =
    let nonEmpty = stringToOption s
    let nonEmptyUpper = Option.map toUpper nonEmpty
    nonEmptyUpper

let storeInDatabase (path: string) (content: string) : string option =
    try
        System.IO.File.WriteAllText(path, content)
        Some content
    with ex ->
        None

let nonEmptyStringStoreInPersistenceAndToUpper (path: string) (content: string) : string option =
    let (nonEmpty: string option) = stringToOption content
    let (stored: string option) = Option.bind (storeInDatabase path) nonEmpty
    let (nonEmptyUpper: string option) = Option.map toUpper stored
    nonEmptyUpper

let sum (a: int) (b: int) (c: int) : int = a + b + c

let onlyPositive (i: int) : int option = if i > 0 then Some i else None

let addNumbers (a: int) (b: int) (c: int) : int option =
    let positiveA = onlyPositive a
    let positiveB = onlyPositive b
    let positiveC = onlyPositive c

    // sum ist vom Typ: (int -> int -> int -> int)
    // jede Zeile füllt ein Argument mehr aus
    // (Partial Application dank Currying)
    let (sum': (int -> int -> int) option) = Option.map sum positiveA
    let (sum'': (int -> int) option) = Option.apply sum' positiveB
    let (sum''': (int) option) = Option.apply sum'' positiveC
    sum'''

let tryParseEmail (input: string) : Result<string, string> =
    if input.Contains '@' then
        Ok input
    else
        Error $"'{input}' is not a valid email"

let tryParseCompanyEmail (input: string) : Result<string, string> =
    if input.Contains "company" then
        Ok input
    else
        Error $"'{input}' is not a valid company email"

let mail = "foo@bar.baz"

let result =
    match tryParseEmail mail with
    | Error e -> e
    | Ok m ->
        match tryParseCompanyEmail m with
        | Error e2 -> e2
        | Ok mc -> mc

let result2 = mail |> tryParseEmail |> Result.bind tryParseCompanyEmail

let bind f m =
    match m with
    | Error e -> Error e
    | Ok x -> f (x)
