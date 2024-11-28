namespace DemoCICD.Domain.Exceptions;

/// <summary>
/// Lớp xử lí các ngoại lệ của Product.
/// </summary>
public static class ProductException
{
    /// <summary>
    /// Không tìm thấy sản phẩm theo Id.
    /// </summary>
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid productId)
            : base($"Product với id {productId} không tìm thấy")
        {
        }
    }
}
