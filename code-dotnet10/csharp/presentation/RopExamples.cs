using CSharpFunctionalExtensions;

namespace presentation;

public static class RopExamples
{
    public static Result<string> TryParseEmail(string unvalidated) =>
        unvalidated.Contains("@")
            ? Result.Success<string>(unvalidated)
            : Result.Failure<string>($"Invalid email address: {unvalidated}");

    public static Result<string> TryParseCompanyEmail(string mail) =>
        mail.Contains("company")
            ? Result.Success<string>(mail)
            : Result.Failure<string>($"Invalid company email address: {mail}");

    public static Result<string> Combined1a()
    {
        string input = "foo@bar.baz";

        var result =
            TryParseEmail(input)
                .Match(
                    onFailure: error => Result.Failure<string>(error),
                    onSuccess: email =>
                        TryParseCompanyEmail(email)
                            .Match(
                                onFailure: e2 => Result.Failure<string>(e2),
                                onSuccess: companyMail => Result.Success(companyMail)));

        return result;
    }

    public static Result<string> Combined1b()
    {
        string input = "foo@bar.baz";

        var result =
            TryParseEmail(input)
                .Match(
                    onFailure: Result.Failure<string>,
                    onSuccess: email =>
                        TryParseCompanyEmail(email)
                            .Match(
                                onFailure: Result.Failure<string>,
                                onSuccess: Result.Success<string>));

        return result;
    }

    public static Result<string> Combined2a()
    {
        string input = "foo@bar.baz";

        var result =
            TryParseEmail(input)
                .Bind(email => TryParseEmail(email));

        return result;
    }

    public static Result<string> Combined2b()
    {
        string input = "foo@bar.baz";

        var result =
            TryParseEmail(input)
                .Bind(TryParseEmail);

        return result;
    }

    public static Result<T> MyBind<T>(
        this Result<T> input, Func<T, Result<T>> func) =>
        input.IsFailure
            ? Result.Failure<T>(input.Error)
            : func(input.Value);

    public static Result<TSuccess, TError> MyBind2<TSuccess, TError>(
        this Result<TSuccess, TError> input, Func<TSuccess, Result<TSuccess, TError>> func) =>
        input.IsFailure
            ? Result.Failure<TSuccess, TError>(input.Error)
            : func(input.Value);
}