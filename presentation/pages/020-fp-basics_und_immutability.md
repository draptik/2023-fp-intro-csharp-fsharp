## Grundlagen: FP 101

- Immutability
- Functions as First Class Citizens
- Pure Functions
- Komposition

<img
  class="absolute bottom-10 right-0 w-140"
  src="/images/strandkind_muecke-woods-7661735.jpg" >

---

## Immutability in C#

Damit ein (C#) Objekt unveränderlich wird, muss gewährleistet sein, dass es auch **nach Erstellung nicht verändert wird**.

- interne Werte dürfen ausschließlich vom Konstruktor verändert werden
- keine public oder private setters
- kein parameterloser Konstrukor

---

### Immutability in C# - Beispiel

```csharp
public class Customer
{
    public string Name { get; set; } // set -> mutable 😡
}
```

vs

```csharp
public class Customer
{
    public Customer(string name)
    {
        Name = name;
    }

    public string Name { get; } // <- immutable 😀
}
```

---

### Records für Immutability?

C# records sind ein erster Schritt in die richtige Richtung:

- Verfügbar ab C# 9
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
