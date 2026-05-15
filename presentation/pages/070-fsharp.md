# Einführung in F#

![img](/images/fsharp256.png)

---

## Warum F#?

- weniger syntaktisches Rauschen
- FP als default
- "pit of success"

---

## F#

- erzwingt keine puren Funktionen, sondern erlaubt Seiteneffekte
- Statisch typisiert
- integriert ins .NET Ökosystem
- C# / VB.NET Interop

<img
  class="absolute top-10 right-10 w-100"
  src="/images/fsharp256.png"
/>

---

## Besonderheiten

- Significant whitespace
- Reihenfolge der Definitionen in Datei wichtig
- Reihenfolge der Dateien im Projekt wichtig

<img
  class="absolute top-10 right-10 w-100"
  src="/images/fsharp256.png"
/>

---

## Immutability als Default

```fsharp
// Achtung: = ist hier keine Zuweisung, sondern heißt 
// "linke und rechte Seite sind gleich und bleiben es auch immer"
// (oder wie in Mathe: "es sei x gleich 3")
let x = 3
let add a b = a + b
let m = if 3 > 0 then 7 else 42

// Mutability nur auf Wunsch - normalerweise unnötig
let mutable y = 3
y <- 42
```

<img
  class="absolute bottom-10 right-10 w-60 opacity-30"
  src="/images/fsharp256.png"
/>

---

## Typ-Inferenz

```fsharp
// Typen werden automatisch geschlussfolgert sofern möglich
let timesTwo a = a * 2 // int -> int

// Explizite Angaben möglich
let timesTwoExplicit (a: int) : int = a * 2
//                       ^^^  ^^^^^
```

<img
  class="absolute bottom-10 right-10 w-60 opacity-30"
  src="/images/fsharp256.png"
/>

---

## Pipe-Operator

```fsharp
// der letzte Parameter kann mit dem Ergebnis 
// der vorherigen Expression ausgefüllt werden
let triple a = a * 3
4 |> triple // ergibt 12
4 |> triple |> triple // ergibt 36
```

<img
  class="absolute bottom-10 right-10 w-60 opacity-30"
  src="/images/fsharp256.png"
/>

---

## Discriminated Unions

- vielleicht auch in C# 15?

```fsharp
// Discriminated Unions (aka "Tagged Union", "Sum Type", "Choice Type")
type Vehicle = Bike | Car | Bus

// Pattern Matching zur Behandlung der verschiedenen Fälle
let vehicle = Bike
let laneText = 
    match vehicle with
    | Bike -> "Use the bike lane"
    | Car -> "Use the car lane"
    | Bus -> "The bus uses its own lane"
```

<img
  class="absolute bottom-10 right-10 w-60 opacity-30"
  src="/images/fsharp256.png"
/>

---

## Discriminated Unions mit Werten

```fsharp
// auch mit unterschiedlichen(!) Daten an jedem Fall möglich
type Shape =
    | Circle of float
    | Rectangle of float * float

let shape = Circle 42.42

match shape with
| Circle radius -> radius * radius * System.Math.PI
| Rectangle(width, height) -> width * height
```

<img
  class="absolute bottom-10 right-10 w-60 opacity-30"
  src="/images/fsharp256.png"
/>

---

## Record Types

```fsharp
// Record Type
type ShoppingCart = {
    products: Product list
    total: float
    createdAt: System.DateTime
}

// Typ muss nur angegeben werden wenn er nicht eindeutig ist
// hier wird er z.B. automatisch erkannt
let shoppingCart = {
    products = []
    total = 42.42
    createdAt = System.DateTime.Now
}
```

<img
  class="absolute bottom-10 right-10 w-60 opacity-30"
  src="/images/fsharp256.png"
/>

---

## Record Types

- Immutable by default
- Unmöglich einen ungültigen Record zu erzeugen
- Structural Equality als default
- Hint: C# Value Objects out of the box
- ✨ seit C# 9: `records`

<img
  class="absolute bottom-10 right-10 w-60 opacity-30"
  src="/images/fsharp256.png"
/>

---

## Es gibt noch mehr

- Currying
- Partial Application
- Computation Expressions
- Active Patterns
- Units of Measure