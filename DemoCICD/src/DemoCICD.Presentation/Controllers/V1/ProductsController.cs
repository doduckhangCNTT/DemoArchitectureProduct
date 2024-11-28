using Asp.Versioning;
using DemoCICD.Contract.Extensions;
using DemoCICD.Contract.Services.V1.Product;
using DemoCICD.Contract.Shared;
using DemoCICD.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoCICD.Presentation.Controllers.V1;

[ApiVersion(1)]
public class ProductsController : ApiController
{
    public ProductsController(ISender sender) : base(sender)
    {
    }

    [HttpPost(Name = "CreateProducts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Products([FromBody] Command.CreateProductCommand CreateProduct)
    {
        var result = await Sender.Send(CreateProduct);

        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    /// <summary>
    /// Lấy toàn bộ products.
    /// </summary>
    /// <returns>Danh sách products.</returns>
    [HttpGet(Name = "GetProducts")]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.ProductResponse>>), StatusCodes.Status200OK)] // Mô tả trên Swagger giá trị trả về khi status 200
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Products(
        string? searchTerm = null,
        string? sortColumn = null,
        string? sortOrder = null,
        string? sortColumnAndOrder = null,
        int pageIndex = 1,
        int pageSize = 10)
    {
        var result = await Sender.Send(new Query.GetProductsQuery(
            searchTerm,
            sortColumn,
            SortOrderExtension.ConvertStringToSortOrder(sortOrder),
            SortOrderExtension.ConvertStringToSortOrderV2(sortColumnAndOrder),
            pageIndex, pageSize));

        return Ok(result);
    }

    /// <summary>
    /// Lấy thông tin sản phẩm theo Id.
    /// </summary>
    /// <param name="productId">Mã sản phẩm.</param>
    /// <returns>Sản phẩm.</returns>
    [HttpGet("{productId}")]
    [ProducesResponseType(typeof(Result<Response.ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Products(Guid productId)
    {
        var result = await Sender.Send(new Query.GetProductByIdQuery(productId));
        return Ok(result);
    }

    /// <summary>
    /// Xóa sản phẩm theo Id.
    /// </summary>
    /// <param name="productId">Mã sản phẩm.</param>
    /// <returns>Trạng thái sau khi xóa.</returns>
    [HttpDelete("{productId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProducts(Guid productId)
    {
        var result = await Sender.Send(new Command.DeleteProductCommand(productId));
        return Ok(result);
    }

    /// <summary>
    /// Cập nhật thông tin sản phẩm.
    /// </summary>
    /// <param name="productId">Mã sản phẩm cập nhật.</param>
    /// <param name="updateProduct">Thông tin sản phẩm cập nhật.</param>
    /// <returns>Trạng thái cập nhật.</returns>
    [HttpPut("{productId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Products(Guid productId, [FromBody] Command.UpdateProductCommand updateProduct)
    {
        var updateProductCommand = new Command.UpdateProductCommand(productId, updateProduct.Name, updateProduct.Price, updateProduct.Description);
        var result = await Sender.Send(updateProductCommand);
        return Ok(result);
    }
}
