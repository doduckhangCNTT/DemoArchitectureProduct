namespace DemoCICD.Contract.Shared;

/// <summary>
/// Kết quả có giá trị trả về.
/// </summary>
/// <typeparam name="TValue">Kiểu giá trị trả về.</typeparam>
public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error) =>
        _value = value;

    /// <summary>
    /// Thành công => kết quả.
    /// </summary>
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public static implicit operator Result<TValue>(TValue? value) => Create(value);
}
