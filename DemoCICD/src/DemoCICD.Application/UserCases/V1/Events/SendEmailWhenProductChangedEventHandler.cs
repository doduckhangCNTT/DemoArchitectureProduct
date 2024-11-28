using DemoCICD.Contract.Abstractions.Message;
using DemoCICD.Contract.Services.V1.Product;

namespace DemoCICD.Application.UserCases.V1.Events;
public class SendEmailWhenProductChangedEventHandler :
    IDomainEventHandler<DomainEvent.ProductCreated>,
    IDomainEventHandler<DomainEvent.ProductDelete>
{
    /// <summary>
    /// Xử lí gửi email khi tạo product.
    /// </summary>
    /// <param name="notification">Thông tin gửi.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task Handle(DomainEvent.ProductCreated notification, CancellationToken cancellationToken)
    {

        await Task.Delay(1000);
    }

    /// <summary>
    /// Xử lí gửi email khi xóa product.
    /// </summary>
    /// <param name="notification">Thông tin gửi.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task Handle(DomainEvent.ProductDelete notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
