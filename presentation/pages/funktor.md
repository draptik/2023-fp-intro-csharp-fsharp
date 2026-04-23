# Grundlegende Konzepte der funktionalen Komposition

todo: bild einfügen

---

## "Programming Patterns" in FP

![Scott Wlaschin shows FP patterns in one of his talks](/images/patterns-and-principles-in-fp.png)

----

## Kleine Funktionen zu größeren verbinden

- Gängige Vorgehensweise: Kleine Funktionen werden zu immer größeren Funktionalitäten zusammengesteckt ("Komposition")
- Problem: Nicht alle Funktionen passen gut zusammen

----

### Problem: Wert in Container, Funktion kann nichts damit anfangen

```csharp
// C#

using CSharpFunctionalExtensions;

public class X
{
        string ToUpper(string s) => s.ToUpper();

        Maybe<string> StringToMaybe(string s)
            => string.IsNullOrEmpty(s) ? Maybe.None : Maybe.From(s);

        Maybe<string> NonEmptyStringToUpper(string s)
        {
            var nonEmpty = StringToMaybe(s);
            // passt nicht: Type "string" erwartet, 
            // aber "Maybe<string>" aus StringToMaybe bekommen
            return ToUpper(nonEmpty); // 💥
        }
}
```

----

### Problem: Wert in Container, Funktion kann nichts damit anfangen

```fsharp
// F#
module X

let toUpper (s: string) : string = s.ToUpper()

let stringToOption (s: string) : string option =
    if String.IsNullOrWhiteSpace s then
        None
    else
        Some s

let nonEmptyStringToUpper (s: string) : ??? =
    let (nonEmpty : string option) = stringToOption s
    // passt nicht: "string" erwartet, 
    // aber "string option" bekommen
    let nonEmptyUpper = toUpper nonEmpty // 💥
```

----

### Funktor ("Mappable")

![img](/images/Funktor_1.png)

----

### Funktor ("Mappable")

- Container mit "map" Funktion (die bestimmten Regeln folgt): "Mappable"
- Bezeichnung in der FP-Welt: **Funktor**

```fsharp
  map: (a -> b) -> F a -> F b
```

- Andere Bezeichnungen für "map": fmap (z.B. in Haskell), Select (LINQ), &lt;$&gt;, &lt;!&gt;

----

### Funktor = Lösung für "Wert in Container, Funktion kann nichts damit anfangen"

- Option.map
- List.map, Seq.map, Result.map, ...

```fsharp
let toUpper (s: string) : string = s.ToUpper()

let stringToOption (s: string) : string option =
    if String.IsNullOrWhiteSpace s then
        None
    else
        Some s

let nonEmptyStringToUpper (s: string) : string option =
    let nonEmpty = stringToOption s
    // Lösung: map-Funktion nutzen
    let nonEmptyUpper = Option.map toUpper nonEmpty
```

----

### Beispiel nochmal in C#

```csharp

using CSharpFunctionalExtensions;

public class X
{
        string ToUpper(string s) => s.ToUpper();

        Maybe<string> StringToMaybe(string s)
            => string.IsNullOrEmpty(s) ? Maybe.None : Maybe.From(s);

        Maybe<string> NonEmptyStringToUpper(string s)
        {
            var nonEmpty = StringToMaybe(s);
           
           // Lösung: Map-Funktion nutzen
            return nonEmpty
                .Map(ToUpper);
        }
}

```