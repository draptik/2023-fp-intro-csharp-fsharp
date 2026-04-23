using CSharpFunctionalExtensions;

Console.WriteLine(AddNumbers(1, 2, 3));
Console.WriteLine(AddNumbers(-1, -2, -3));
return;

int Sum(int a, int b, int c) => a + b + c;

Result<int> OnlyPositive(int i)
    => i > 0
        ? Result.Success(i)
        : Result.Failure<int>($"Number {i} is not positive.");

Result<int> AddNumbers(int a, int b, int c)
{
    var ra = OnlyPositive(a);
    var rb = OnlyPositive(b);
    var rc = OnlyPositive(c);

    var combineResult = Result.Combine("\n", ra, rb, rc);
    return combineResult.IsFailure
        ? Result.Failure<int>(combineResult.Error)
        : Result.Success(Sum(a, b, c));
}