# Einführung in F#

![img](/images/fsharp256.png)

---

## Warum F#?

- weniger syntaktisches Rauschen
- FP als default
- "pit of success"

---

## F#

- Ursprünglich: Microsoft Forschungsprojekt
- Heute: Community-driven
- inspiriert von OCaml
- Multi-Paradigma
- Fokus auf funktionale Programmierung

<img
  class="absolute top-10 right-10 w-100"
  src="/images/fsharp256.png"
/>

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

## Currying

> Currying ist die Umwandlung einer Funktion mit mehreren Parametern in eine neue Funktion mit nur einem Parameter, die wiederum eine Funktion zurückgibt mit dem Rest der Parameter.

```fsharp
// int -> int -> int -> int
// eigentlich: int -> (int -> (int -> int))
let addThree a b c = a + b + c
```

<img
  class="absolute bottom-10 right-10 w-60 opacity-30"
  src="/images/fsharp256.png"
/>

---

## Partial Application

- Eine Funktion mit mehreren Parametern bekommt nur einen Teil ihrer Argumente übergeben - der Rest bleibt offen und kann später ausgefüllt werden
- klappt nur so einfach wegen automatischem Currying in F#

```fsharp
// Partial Application
let add a b = a + b // int -> (int -> (int))

let add2 = add 2 // (int -> (int)), "a" ist mit 2 ausgefüllt, "b" ist noch offen
let six = add2 4 // (int)
let ten = add2 8 // (int)
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
- Structural Equality
- Hint: C# Value Objects out of the box
- ✨ seit C# 9: `records`

<img
  class="absolute bottom-10 right-10 w-60 opacity-30"
  src="/images/fsharp256.png"
/>

---

## Structural Equality

```fsharp
// Structural Equality
type Thing = {content: string; id: int}
let thing1 = {content = "abc"; id = 15}
let thing2 = {content = "abc"; id = 15}
let equal = (thing1 = thing2) // true
```

- Record Types mit Structural Equality sind ideal, um sehr kompakt "Value Objects" ausdrücken zu können

<img
  class="absolute bottom-10 right-10 w-60 opacity-30"
  src="/images/fsharp256.png"
/>

---

## Structural Equality vs. DDD Aggregates

- Möchte man die Standard-Equality nicht, ist es best practice, Equality und Comparison zu verbieten
- dann muss explizit auf eine Eigenschaft verglichen werden (z.B. die Id)

```fsharp
[<NoEquality; NoComparison>]
type NonEquatableNonComparable = {
    Id: int
}

let compare n1 n2 = (n1.Id = n2.Id)
```

<img
  class="absolute bottom-10 right-10 w-60 opacity-30"
  src="/images/fsharp256.png"
/>
