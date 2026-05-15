## Option: Mögliches Vorhandensein eines Werts

...oder: NULL ist gefährlich (["Billion-Dollar Mistake"](https://en.wikipedia.org/wiki/Tony_Hoare#Research_and_career))

<streamline-ultimate-money-bag-dollar-bold class="absolute top-45 right-70 text-[80px]"/>

<img
  class="absolute top-30 left-15 w-130"
  src="/images/meme-null.png"
/>

<img
  class="absolute top-33 right-15 w-50"
  src="/images/Sir_Tony_Hoare.jpg"
/>

---
layout: two-cols-header-with-footer
---

### Enthält die Signatur die ganze Wahrheit?

::left::

```csharp
// Enthält die Signatur die ganze Wahrheit?
public string Stringify<T>(T data)
{
    return null;
}
```

::right::

```csharp
// Sind Magic Values eine gute Idee?
public int Intify(string s)
{
    int result = -1;
    int.TryParse(s, out result);
    return result;
}
```

---

## Option

```fsharp
// Definition in F#
type Option<'T> = Some<'T> | None
```

- entweder ein Wert vom Typ T ist da - dann ist er in "Some" eingepackt
- oder es ist kein Wert da, dann gibt es ein leeres "None"
- alternative Bezeichnungen: Optional, Maybe

---

## Mit Option

```csharp
public Option<int> IntifyOption(string s)
{
    bool success = int.TryParse(s, out var result);
    return success
        ? Some(result)
        : None;
}
```

---

### Wie komme ich an einen eingepackten Wert ran?

> **Pattern matching** allows you to match a value against some patterns to select a branch of the code.

```csharp
public string Stringify<T>(Option<T> data)
{
    return data.Match(
        None: () => "",
        Some: (existingData) => existingData.ToString()
    );
}
```

---

### Vorteile

<v-clicks :fade="true">

- Explizite Semantik: Wert ist da - oder eben nicht
- Die Signatur von Match erzwingt eine Behandlung beider Fälle - nie wieder vergessene Null-Checks!
- Achtung: In C# bleibt das Problem, dass "Option" auch ein Objekt ist - und daher selbst null sein kann
- daher mindestens: in C# explizites NULL enablen mit `<Nullable>enable</Nullable>`

</v-clicks>
