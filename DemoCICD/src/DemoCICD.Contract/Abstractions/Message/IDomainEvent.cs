using MediatR;

namespace DemoCICD.Contract.Abstractions.Message;
public interface IDomainEvent : INotification
{
    public Guid Id { get; init; } // Do muốn chỉ gán lần đầu tiên khi khởi tạo lên sẽ dùng "init"
}
