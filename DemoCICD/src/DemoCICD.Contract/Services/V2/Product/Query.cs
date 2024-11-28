using DemoCICD.Contract.Abstractions.Message;
using DemoCICD.Contract.Enumerations;
using DemoCICD.Contract.Shared;
using static DemoCICD.Contract.Services.V2.Product.Response;

namespace DemoCICD.Contract.Services.V2.Product;

/// <summary>
/// Lớp truy vấn (Query) sản phẩm.
/// </summary>
public static class Query
{
    /// <summary>
    /// Lớp lấy danh sách sản phẩm
    /// </summary>
    /// <param name="SearchTerm">Gía trị search</param>
    /// <param name="SortColumn">Cột sắp xếp</param>
    /// <param name="SortOrder">Kiểu sắp xếp trên cột</param>
    /// <param name="SortColumnAndOrder">Danh sách cột và kiểu sắp xếp dạng key-value</param>
    public record GetProductsQuery() : IQuery<Result<List<ProductResponse>>>;

    /// <summary>
    /// Lớp lấy sản phẩm theo id
    /// </summary>
    /// <param name="Id"></param>
    public record GetProductByIdQuery(Guid Id): IQuery<ProductResponse>;
}
