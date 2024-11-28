using DemoCICD.Contract.Shared;
using MediatR;

namespace DemoCICD.Contract.Abstractions.Message;

/// <summary>
/// Truy vấn Handler
/// </summary>
/// <typeparam name="TQuery">Class xử lí</typeparam>
/// <typeparam name="TResponse">Kiểu trả về</typeparam>
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse> // Do IQuery là kiểu Generic lên cần có 1 ràng buộc cho kiểu này
{
}
