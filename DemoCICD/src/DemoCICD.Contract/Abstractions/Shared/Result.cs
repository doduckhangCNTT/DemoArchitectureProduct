namespace DemoCICD.Contract.Shared;

/// <summary>
/// Kết quả trả về.
/// </summary>
public class Result
{
    #region Fields
    /// <summary>
    /// Trạng thái thành công.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Chứa thông tin lỗi xảy ra.
    /// </summary>
    public Error Error { get; }
    #endregion

    #region Constructor
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    } 
    #endregion

    public bool IsFailure => !IsSuccess;

    /////////////////// Thành công ///////////////////

    /// <summary>
    /// Thành công không có giá trị trả về.
    /// </summary>
    /// <returns>Result.</returns>
    public static Result Success() => new(true, Error.None);

    /// <summary>
    /// Thành công có giá trị trả về.
    /// </summary>
    /// <typeparam name="TValue">Kiểu giá trị trả về.</typeparam>
    /// <param name="value">Giá trị trả về.</param>
    /// <returns>Result.</returns>
    public static Result<TValue> Success<TValue>(TValue value) =>
        new(value, true, Error.None);

    /////////////////// Thất bại ///////////////////

    /// <summary>
    /// Thất bại không có giá trị trả về.
    /// </summary>
    /// <param name="error">Thông tin lỗi.</param>
    /// <returns>Result.</returns>
    public static Result Failure(Error error) =>
        new(false, error);

    /// <summary>
    /// Thất bại có kết quả trả về.
    /// </summary>
    /// <typeparam name="TValue">Kiểu giá trị trả về.</typeparam>
    /// <param name="error">Thông tin lỗi.</param>
    /// <returns>Result.</returns>
    public static Result<TValue> Failure<TValue>(Error error) =>
        new(default, false, error);

    /// <summary>
    /// Trả về trạng thái Success/Failure theo giá trị có null hay không.
    /// </summary>
    /// <typeparam name="TValue">Kiểu giá trị trả về.</typeparam>
    /// <param name="value">Giá trị trả về.</param>
    /// <returns>Result.</returns>
    public static Result<TValue> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
}
