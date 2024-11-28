namespace DemoCICD.Contract.Shared;

/// <summary>
/// Lớp validate có giá trị trả về.
/// </summary>
/// <typeparam name="TValue">Kiểu trả về.</typeparam>
public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
{
    public Error[] Errors { get; }

    private ValidationResult(Error[] errors)
        : base(default, false, IValidationResult.ValidationError) =>
        Errors = errors;

    /// <summary>
    /// Lấy thông tin lỗi.
    /// </summary>
    /// <param name="errors">Thông tin lỗi.</param>
    /// <returns>ValidationResult.</returns>
    public static ValidationResult<TValue> WithErrors(Error[] errors) => new (errors);

}
