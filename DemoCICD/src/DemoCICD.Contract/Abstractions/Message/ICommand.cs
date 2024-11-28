using DemoCICD.Contract.Shared;
using MediatR;

namespace DemoCICD.Contract.Abstractions.Message;

/// <summary>
/// Kiểu command không có giá trị trả về.
/// </summary>
public interface ICommand : IRequest<Result>
{
}

/// <summary>
/// Kiểu command có giá trị trả về.
/// </summary>
/// <typeparam name="TResponse">Kiểu dữ liệu giá trị trả về.</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
