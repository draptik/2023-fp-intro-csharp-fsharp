## FP 101 - Functions as first class citizens

Funktionen können als Parameter und Rückgabewert verwendet werden

Klassische OOP-Lösung: Strategy-Pattern

```csharp
interface ICalculateSalary
{
    int ByInput(int i);            // <- Methodensignatur
}

class Manager: ICalculateSalary
{
    int ByInput(int i) => i*2;     // <- Implementierung
}
```

```csharp
class SomeService
{
    int DoSomething(ICalculateSalary salary, int i) // <- "inject"
        => salary.ByInput(i);                       // <- "deligiert"
}
```

(Verhalten als Parameter übergeben)

---
layout: two-cols-header
---

## FP 101 - Functions as first class citizens

...geht auch "funktional" in C# 😎

::left::

```csharp
// Funktion empfangen
double CalcSalary(Func<int, double> calc, int salary) 
    => calc(salary);
```

::right::

```csharp
// Funktion zurückgeben
Func<double, string> GetConverterFn(bool withDecimals) 
    => withDecimals 
        ? ToStringWithDecimals 
        : ToStringNoDecimals;
```

<style>
.two-cols-header {
  column-gap: 15px;
}
</style>
