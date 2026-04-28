## Railway Oriented Programming (1/n)

Eine Metapher für funktionale Fehlerbehandlung

- man geht davon aus, dass Fehler passieren
- der Datentyp "Discriminated Unions" bietet eine gute Grundlage für einen neuen Datentyp
- in Kombination mit "Functions as 1st class citizens" entstehen neue Patterns...

---
layout: two-cols-header
---

## Railway Oriented Programming (2/n)

Konzept 1: ein "Result" Typ, mit zwei Ausgabe-Zuständen:

::left::

```fsharp
type Result<'Ok, 'Error> =
  | Ok of 'Ok
  | Error of 'Error
```

::right::

```fsharp
type UnvalidatedCustomer = { Name: string }
type Customer = { Name: string }

let signup (unvalidated: UnvalidatedCustomer) : Result<Customer, string> =
  if String.IsNullOrEmpty unvalidated.Name then
    Error "Ups"
  else
    Ok Customer { Name = unvalidated }
```

<style>
.two-cols-header {
  column-gap: 15px;
}
</style>

---
layout: two-cols-header
---

## Railway Oriented Programming (3/n)

foo

::left::

```fsharp
type Result<'Ok, 'Error> =
  | Ok of 'Ok
  | Error of 'Error
```

::right::

```fsharp
type SuperCustomer = { Name: string }

let parseSuperCustomer customer : Result<SuperCustomer, string> =
  if customer.Name <> "super" then
    Error "Ups - not a super customer"
  else
    Ok SuperCustomer { Name = customer }
```

<style>
.two-cols-header {
  column-gap: 15px;
}
</style>

---
layout: two-cols-header
---

## Railway Oriented Programming (4/n)

Konzept 2: Funktionen, die damit umgehen können (parse, don't validate)

::left::

```fsharp
// TODO
```

::right::

TODO

<style>
.two-cols-header {
  column-gap: 15px;
}
</style>
---

- In Railway-Sprech bedeutet dass, dass man "zweigleisig" fährt:
- Jede **Funktion** bekommt eine Eingabe, und
  - hat "im Bauch" eine Weiche, die entscheidet ob
    - auf das Fehlergleis oder
    - auf das Erfolgsgleis umgeschaltet wird.
- Die Wrapperklasse mit der **Funktion** ist das Entscheidende!

---

- In anderen Worten: die Funktionen haben aktuell 1 Eingabe (1 Gleis), und 2 Ausgaben (2 Gleise)

<img
  class="absolute bottom-50 left-10 w-200"
  src="/images/rop-tracks-Page-2.png"
/>

---

- Man benötigt also einen Mechanismus, der eine 2-gleisige Ausgabe so umwandelt, dass eine Funktion, die eine 1-gleisige Eingabe erwartet, damit umgehen kann

<img
  class="absolute bottom-10 left-20 w-180"
  src="/images/rop-tracks-Page-4.png"
/>

---

### Was muss dieser Mechanismus können?

- wenn die Eingabe fehlerhaft ist, muss die Funktion nichts tun, und kann den Fehler weiterreichen
- wenn die Eingabe nicht fehlerhaft ist, wird der Wert an die Funktion gegeben
- Damit können wir elegant beliebig lange Ketten bauen

```csharp
public static Result<TIn, TOut> Bind(this Result<TIn, TOut>, Func<Tin, TOut> f)
{
  // TODO
}
```
