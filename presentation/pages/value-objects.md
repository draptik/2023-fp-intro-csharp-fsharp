## Immutability in C#

Damit ein C# Objekt unveränderlich wird, muss gewährleistet sein, dass es auch **nach Erstellung nicht verändert wird**.

- interne Werte dürfen ausschließlich vom Konstruktor verändert werden
- keine public oder private setters
- kein parameterloser Konstrukor

---

### C# 9 and greater...

C# records sind ein erster Schritt in die richtige Richtung:

- Equality by structure
- immutable
- spezielle Syntax, um eine veränderte **Kopie** zu erzeugen:

```csharp
var x2 = x1 with {Value = 42};
```

---

### Equality by structure

Zwei Objekte sind gleich, wenn sie die gleichen Werte haben.

- `Equals` und `GetHashcode` überschreiben

```csharp
override bool Equals(Geld other)
    => other.Betrag   == this.Betrag &&
       other.Waehrung == this.Waehrung;

override int GetHashCode() { /* ... */ }
```

---

### Exkurs

- ⚠️ Unterschied OOP vs FP ⚠️
  - OOP: **Objekte sollen Verhalten besitzen** (anämische Modelle sind ein Antipattern in der OO-Welt)
  - FP: Üblicherweise **Trennung von Datenstruktur und Verhalten**

Das Schöne an den unterschiedlichen Paradigmen ist:

- man kann es situationsbedingt einfach lösen
- Und sich das Beste rauspicken

---

### Vogen

- "A semi-opinionated library which is a source generator and a code analyser. It Source generates Value Objects"
- der mitgelieferte Code Analyser verbietet eigene Konstruktoren, erkennt falsche Nutzung des Value Objects, ...
- Standardimplementierungen für gängige Serialisierungen (JSON, EF Core, MongoDB, ...)
- <https://stevedunn.github.io/Vogen/vogen.html>

---

### Vogen

```csharp
[ValueObject<int>] // <--
public partial struct CustomerId;

CustomerId customerId = CustomerId.From(123);

// Eigene Validierungslogik
private static Validation Validate(int input) => input > 0
    ? Validation.Ok
    : Validation.Invalid("Customer IDs must be greater than 0.");

// besondere Werte möglich, die dann als eine Art Enum generiert werden
[ValueObject<float>]
[Instance("Freezing", 0)]
[Instance("Boiling", 100)]
public readonly partial struct Celsius {
    private static Validation Validate(float value) =>
        value >= -273 ? Validation.Ok : Validation.Invalid("Cannot be colder than absolute zero");
}
```
