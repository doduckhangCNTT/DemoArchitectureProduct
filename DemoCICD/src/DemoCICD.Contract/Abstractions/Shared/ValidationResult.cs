namespace DemoCICD.Contract.Shared;

/// <summary>
/// Lớp validate không có kết quả trả về.
/// </summary>
public sealed class ValidationResult : Result, IValidationResult
{
    public Error[] Errors { get; }

    private ValidationResult(Error[] errors)
        : base(false, IValidationResult.ValidationError)
        => Errors = errors;

    public static ValidationResult WithErrors(Error[] errors) => new(errors);
}
