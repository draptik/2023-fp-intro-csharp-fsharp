# Grundlegende Konzepte der funktionalen Komposition

<img
  class="absolute top-25 right-10 w-130"
  src="/images/cartoon_fp_scene.svg"
/>

---

## "Programming Patterns" in FP

![Scott Wlaschin shows FP patterns in one of his talks](/images/patterns-and-principles-in-fp.png)

---

## Kleine Funktionen zu größeren verbinden 🧑

- Gängige Vorgehensweise: Kleine Funktionen werden zu immer größeren Funktionalitäten zusammengesteckt ("Komposition")
- Problem: Nicht alle Funktionen passen gut zusammen

---

### Problem: Wert in Container, Funktion kann nichts damit anfangen (C#)

```csharp
// C#
using CSharpFunctionalExtensions;

public class X
{
    // erwartet als Parameter den Typ "string":
    string ToUpper(string s) => s.ToUpper();

    // Ergebnis ist "eingepackt":
    Maybe<string> StringToMaybe(string s) => string.IsNullOrEmpty(s) ? Maybe.None : Maybe.From(s);

    // Versuch, die Methoden zu kombinieren:
    Maybe<string> NonEmptyStringToUpper(string s)
    {
        var nonEmpty = StringToMaybe(s); // <- gibt "eingepackten" Wert zurück

        return ToUpper(nonEmpty); // 💥 passt nicht: Type "string" erwartet,
                                  // aber "Maybe<string>" aus StringToMaybe bekommen
    }
}
```

---

### Funktor ("Mappable")

![img](/images/Funktor_1.png)

---

### Funktor ("Mappable")

- Container mit "map" Funktion (die bestimmten Regeln folgt): "Mappable"
- Bezeichnung in der FP-Welt: **Funktor**

```fsharp
    map: (a -> b) -> F a -> F b
```

- `(a -> b)`: Funktion, die `a` bekommt, und `b` zurückgibt
- `F a`: `a` in einen Funktor `F` verpackt
- `F b`: `b` in einen Funktor `F` verpackt
- Andere Bezeichnungen für "map":<br> `fmap` (z.B. in Haskell), `Select` (LINQ), `<$>`, `<!>`

---

### Funktor = Lösung für "Wert in Container, Funktion kann nichts damit anfangen"

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
         // ^^^^
    }
}
```

---

### Beispiel in F#

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
                     // ^^^^^^^^^^
```
