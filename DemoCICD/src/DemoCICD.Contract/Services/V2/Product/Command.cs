using DemoCICD.Contract.Abstractions.Message;

namespace DemoCICD.Contract.Services.V2.Product;

/// <summary>
/// Lớp command sản phẩm
/// </summary>
public static class Command
{
    /// <summary>
    /// Lớp tạo sản phẩm
    /// </summary>
    /// <param name="Name">Tên sản phẩm</param>
    /// <param name="Price">Gía sản phẩm</param>
    /// <param name="Description">Mô tả</param>
    public record CreateProductCommand(string Name, decimal Price, string Description) : ICommand;

    /// <summary>
    /// Lớp cập nhật sản phẩm
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Price"></param>
    /// <param name="Description"></param>
    public record UpdateProductCommand(Guid Id, string Name, decimal Price, string Description) : ICommand;

    /// <summary>
    /// Xóa sản phẩm
    /// </summary>
    /// <param name="Id"></param>
    public record DeleteProductCommand(Guid Id) : ICommand;
}
