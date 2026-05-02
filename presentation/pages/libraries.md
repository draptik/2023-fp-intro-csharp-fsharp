# Funktionale Programmierung in C#

### Was ist heute schon möglich in C#?

- die Entwicklung von C# geht in eine funktionale Richtung
- vieles geht mit Libraries auch heute schon in C#
- F# hat das alles eingebaut

---

### C# 9

- (✅) record types
- 💥 discriminated unions
- (✅) pattern matching
- (✅) immutability / non-nullability

![/images/tweet-don-syme-fsharp-csharp.png](/images/tweet-don-syme-fsharp-csharp.png)

<https://nitter.space/dsymetweets/status/1294280620823240706#m>

---

### C# 15

- Union Types sind in der preview
- Syntax etc. kann sich noch ändern
- noch nicht sicher, ob sie in C# 15 wirklich drin sein werden
- <https://devblogs.microsoft.com/dotnet/csharp-15-union-types/>

```csharp
public record class Cat(string Name);
public record class Dog(string Name);
public record class Bird(string Name, bool Songbird);

public union Pet(Cat, Dog, Bird);

public static string Describe(Pet pet) =>
    pet switch
    {
        Cat cat => $"Cat: {cat.Name}",
        Dog dog => $"Dog: {dog.Name}",
        Bird bird => $"Bird: {bird.Name} is a songbird: {bird.Songbird}"
    };

```

---

### C# Functional Extensions

- Nuget-Paket: [https://github.com/vkhorikov/CSharpFunctionalExtensions](https://github.com/vkhorikov/CSharpFunctionalExtensions)
- hat alles was man braucht, ohne zu kompliziert zu sein
- haben wir erfolgreich in Projekten eingesetzt

---

### Discriminated Unions in C# jetzt schon nutzen

- OneOf [https://github.com/mcintyre321/OneOf](https://github.com/mcintyre321/OneOf)
- dunet [https://github.com/domn1995/dunet](https://github.com/domn1995/dunet)

---
layout: two-cols-header
---

### OneOf

::left::

```csharp
// OneOf
record Rectangle(int Width, int Height);
record Circle(int Radius);

// Discriminated Union
class Shape : OneOfBase<Circle, Rectangle>
{
    private Shape(OneOf<Circle, Rectangle> _) : base(_) {}
    static implicit operator Shape(Rectangle _) => new(_);
    static implicit operator Shape(Circle _) => new(_);
}

// Usage: Match Methode für Pattern Matching
static string Describe(Shape shape) =>
    shape.Match(
        circle => 
            $"Circle has radius {circle.Radius}",
        rectangle => 
            $"H: {rectangle.Height} " +
            $"W: {rectangle.Width}");
```

::right::

```csharp
[Fact]
public void Shape_tests()
{
    Shape shape1 = new Circle(42);
    Shape shape2 = new Rectangle(2, 3);

    var result1 = Describe(shape1);
    var result2 = Describe(shape2);

    result1.Should().Be("Circle has radius 42");
    result2.Should().Be("H: 3 W: 2");
}
```

<style>
.two-cols-header {
  column-gap: 15px;
}
</style>
<!-- <style>
.slidev-code * {
    font-size: smaller !important;
}
</style> -->

---
layout: two-cols-header
---

### dunet

::left::

```csharp
// dunet
using Dunet;

[Union]
partial record Shape
{
    partial record Circle(double Radius);
    partial record Rectangle(double Length, double Width);
}
```

::right::

```csharp
// Usage
using static Shape;

Shape shape = new Rectangle(3, 4);

// inklusive Check, ob alle Cases behandelt werden
var area = shape switch 
{
    Circle(var radius) => 3.14 * radius * radius,
    Rectangle(var length, var width) => length * width,
};

// alternativ zum switch
var areaMatch = shape.Match(
    circle => 3.14 * circle.Radius * circle.Radius,
    rectangle => rectangle.Length * rectangle.Width
);
```

<style>
.two-cols-header {
  column-gap: 15px;
}
</style>
