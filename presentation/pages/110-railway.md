---
layout: image-right
image: "/images/pexels-kunio-hori-691347157-18232626.jpg"
---

## Railway Oriented Programming (1/6) 🧔

Eine Metapher für funktionale Fehlerbehandlung

- man geht davon aus, dass Fehler passieren
- der Datentyp "Discriminated Unions" bietet eine gute Grundlage für einen neuen Datentyp
- in Kombination mit "Functions as 1st class citizens" entsteht etwas Neues

---
layout: two-cols-header-with-footer
---

### Railway Oriented Programming (2/6)

Konzept 1: ein "Result" Typ, mit zwei Ausgabe-Zuständen:

::left::

```fsharp
type Result<'Ok, 'Error> =
    | Ok of 'Ok
    | Error of 'Error
```

::right::

```fsharp
// Beispiel 1
let tryParseEmail (input: string) : Result<string, string> =
    if input.Contains '@' then
        Ok input
    else
        Error $"'{input}' is not a valid email"

// Beispiel 2
let tryParseCompanyEmail (input: string) : Result<string, string> =
    if input.Contains "company" then
        Ok input
    else
        Error $"'{input}' is not a valid company email"
```

---
layout: two-cols-header-with-footer
---

### Railway Oriented Programming (3/6)

Konzept 2: Funktion, die damit umgehen kann - "Bind" (F#-Version)

::left::

```fsharp
let unvalidated = "foo@bar.baz"
```

```fsharp
// "Zu Fuß" - Pyramid of Doom
let result =
    match tryParseEmail unvalidated with
    | Error e1 -> e1
    | Ok mail ->
        match tryParseCompanyEmail mail with
        | Error e2 -> e2
        | Ok companyMail -> companyMail
```

```fsharp
// mit "bind"
// Fokussierung auf "Happy Path", trotzdem mit Fehlerbehandlung
let result =
    unvalidated
    |> tryParseEmail
    |> Result.bind tryParseCompanyEmail
```

::right::

```fsharp
// Vereinfachte "bind" Funktion
let bind f m =
    match m with
    | Error e -> Error e
    | Ok inner -> f(inner)
```

---
layout: two-cols-header-with-footer
---

### Railway Oriented Programming (4/6)

Konzept 2: Funktion, die damit umgehen kann - "Bind" (C#-Version)

::left::

```csharp
string input = "foo@bar.baz";
```

```csharp
// "Zu Fuß" - Pyramid of Doom
var result =
  TryParseEmail(input)
    .Match(
      onFailure: err1 => Result.Failure<string>(err1),
      onSuccess: email =>
        TryParseCompanyEmail(email)
          .Match(
            onFailure: err2 => Result.Failure<string>(err2),
            onSuccess: companyMail => Result.Success(companyMail)));
```

```csharp
// mit "bind"
// Fokussierung auf "Happy Path", trotzdem mit Fehlerbehandlung
var result =
  TryParseEmail(input)
    .Bind(email => TryParseCompanyEmail(email)); 
```

::right::

```csharp
// Vereinfachte "bind" Funktion
public static Result<T> MyBind<T>(
  this Result<T> input,
    Func<T, Result<T>> func) => 
  input.IsFailure 
    ? Result.Failure<T>(input.Error) 
    : func(input.Value);
```

---

### Railway Oriented Programming (5/6)

**Bind** ermöglicht einer Funktion mit 1 Eingabe den Umgang mit Result-Typen (2 Ausgaben)

<img
  class="absolute bottom-10 left-20 w-180"
  src="/images/rop-tracks-Page-4.png"
/>

---

### Railway Oriented Programming (6/6)

- wenn die Eingabe fehlerhaft ist, muss die Funktion nichts tun, und kann den Fehler weiterreichen
- wenn die Eingabe nicht fehlerhaft ist, wird der Wert an die Funktion gegeben
- Damit können wir elegant beliebig lange Ketten bauen

```csharp
// Beispiel
string input = "foo@bar.baz";
var result =
  TryValidateInput(input)
    .Bind(x => TryEnsureUniqueness(x))
    .Bind(x => TryPersistInput(x))
    .Bind(x => TrySendEmail(x));

return result.Match(
  onFailure: /* .. */,
  onSuccess: /* .. */,
);
```
