using CSharpFunctionalExtensions;

namespace presentation;

public class FunctorExample
{
    string ToUpper(string s) => s.ToUpper();

    Maybe<string> StringToMaybe(string s)
        => string.IsNullOrEmpty(s) ? Maybe.None : Maybe.From(s);

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