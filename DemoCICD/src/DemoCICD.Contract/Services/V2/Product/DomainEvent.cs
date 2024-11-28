using DemoCICD.Contract.Abstractions.Message;

namespace DemoCICD.Contract.Services.V2.Product;

/// <summary>
/// Khai báo các phương để có thể lắng nghe sự kiện khi có yêu cầu gửi đến.
/// </summary>
public static class DomainEvent
{
    /// <summary>
    /// Đăng kí lớp tạo Product.
    /// </summary>
    /// <param name="Id"></param>
    public record ProductCreated(Guid Id) : IDomainEvent;

    /// <summary>
    /// Đăng kí lớp cập nhật Product.
    /// </summary>
    /// <param name="Id"></param>
    public record ProductUpdate(Guid Id) : IDomainEvent;

    /// <summary>
    /// Đăng kí lớp xóa Product.
    /// </summary>
    /// <param name="Id"></param>
    public record ProductDelete(Guid Id) : IDomainEvent;
}
