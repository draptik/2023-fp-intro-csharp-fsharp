using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;

namespace presentation;

[SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Local")]
[SuppressMessage("ReSharper", "ArrangeTypeMemberModifiers")]
[SuppressMessage("Performance", "CA1822:Mark members as static")]
public class FunctorExample
{
    string ToUpper(string s)
    {
        return s.ToUpper();
    }

    Maybe<string> StringToMaybe(string s)
    {
        return string.IsNullOrEmpty(s) ? Maybe.None : Maybe.From(s);
    }

    Maybe<string> NonEmptyStringToUpper(string s)
    {
        var nonEmpty = StringToMaybe(s);
        // passt nicht: Type "string" erwartet, aber "Maybe<string>" aus StringToMaybe bekommen
        // return ToUpper(nonEmpty);

        // Lösung: map nutzen
        return nonEmpty
            .Map(ToUpper);
    }
}