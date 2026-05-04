## Grundlagen: FP 101

- Immutability
- Functions as First Class Citizens
- Pure Functions
- Komposition

<img
  class="absolute bottom-10 right-0 w-140"
  src="/images/strandkind_muecke-woods-7661735.jpg" >

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
