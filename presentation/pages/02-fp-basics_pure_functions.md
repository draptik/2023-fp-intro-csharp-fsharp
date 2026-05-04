## FP 101 - Pure Functions

- haben keine Seiteneffekte ("idempotent")
- in C#: sollten immer nach `static` umwandelbar sein
- Beispiele:
  - `int result = 1 + 2` sollte immer das gleiche Ergebnis zurückliefern
  - auch ein HTTP-Endpoint sollte beim erfolgreichen Neuanlegen eines Eintrags
    - immer den Status-Code 201 zurückliefern ("idempotent")
    - auch wenn sich natürlich die Datenbank verändert hat

---
layout: two-cols-header-with-footer
---

## Imperativ...

**Wie** mache ich etwas

::left::

```csharp
var people = new List<Person>
{
    new Person { Age = 20, Income = 1000 },
    new Person { Age = 26, Income = 1100 },
    new Person { Age = 35, Income = 1300 }
};
```

::right::

<v-click>

```csharp
var incomes = new List<int>();
foreach (var person in people)
{
    if (person.Age > 25)
        incomes.Add(person.Income);
}

var avg = incomes.Sum() / incomes.Count;
```

versus...

</v-click>

---
layout: two-cols-header-with-footer
---

## Deklarativ

**Was** will ich erreichen?

::left::

```csharp
var people = new List<Person>
{
    new Person { Age = 20, Income = 1000 },
    new Person { Age = 26, Income = 1100 },
    new Person { Age = 35, Income = 1300 }
}
```

::right::

<v-click>

```csharp
var averageIncomeAbove25 = 
    people
      .Where(p => p.Age > 25) // "Filter"
      .Select(p => p.Income)  // "Map"
      .Average();             // "Reduce"
```

- aussagekräftiger
- weniger fehleranfällig

</v-click>

---

## Pure functions in LINQ

- ihr macht schon FP: LINQ und Lambdas!

<img
  class="absolute bottom-10 right-40 w-190"
  src="/images/pure_functions_hand_drawn_clean.svg" >
