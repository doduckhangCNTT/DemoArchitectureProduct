namespace DemoCICD.Contract.Extensions;
public static class ProductExtension
{
    /// <summary>
    /// Lấy thuộc tính sắp xếp sản phẩm.
    /// </summary>
    /// <param name="sortColumn">Tên cột cần sort.</param>
    /// <returns>Tên cột.</returns>
    public static string GetSortProductProperty(string sortColumn)
        => sortColumn.ToLower() switch
        {
            "name" => "Name",
            "price" => "Price",
            "description" => "Description",
            _ => "Id"
        };
}
