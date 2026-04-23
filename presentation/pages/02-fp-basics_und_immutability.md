# Grundlagen der funktionalen Programmierung

todo: Bild einfügen

<!--
<img
  class="absolute bottom-10 left-20 w-180"
  src="/images/tbd.png"
/>
-->

---

## FP 101

- Functions as First Class Citizens
- (Immutability)
- (Pure Functions)
- (Komposition)

That's it!

---

### Immutability in C#

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

### Syntax matters!

Classic C#

```csharp
int Add(int a, int b)
{
  Console.WriteLine("bla"); // <- side effect!
  return a + b;
}
```

Expression body: Seiteneffekte sind schwieriger reinzubauen

```csharp
int Add(int a, int b) => a + b;
```
