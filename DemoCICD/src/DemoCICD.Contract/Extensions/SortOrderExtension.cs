using DemoCICD.Contract.Enumerations;

namespace DemoCICD.Contract.Extensions;
public static class SortOrderExtension
{
    /// <summary>
    /// Chuyển đổi giá trị sắp xếp thành dạng string sắp xếp mong muốn.
    /// </summary>
    /// <param name="sortOrder"></param>
    /// <returns></returns>
    public static SortOrder ConvertStringToSortOrder(string? sortOrder)
        => !string.IsNullOrWhiteSpace(sortOrder) ?
            (sortOrder.Trim().ToUpper().Equals("ASC") ? SortOrder.Ascending : SortOrder.Descending)
            : SortOrder.Descending;

    /// <summary>
    /// Chuyển đổi dữ liệu string sang dictionary để dễ quản lí sắp xếp.
    /// </summary>
    /// <param name="sortOrder">Dữ liệu sắp xếp.</param>
    /// <returns>Danh sách các thuộc tính cột và kiểu sắp xếp của cột</returns>
    /// <exception cref="FormatException"></exception>
    public static IDictionary<string, SortOrder> ConvertStringToSortOrderV2(string? sortOrder)
    {
        var result = new Dictionary<string, SortOrder>();

        if (!string.IsNullOrEmpty(sortOrder))
        {
            if (sortOrder.Trim().Split(",").Length > 0)
            {
                foreach (var item in sortOrder.Split(","))
                {
                    if (!item.Contains('-'))
                        throw new FormatException("Sort condition should be follow by format: Column1-ASC,Column2-DESC...");
                    var property = item.Trim().Split("-");
                    // Lấy tên cột
                    var key = ProductExtension.GetSortProductProperty(property[0]);
                    // Kiểu sắp xếp trên cột
                    var value = ConvertStringToSortOrder(property[1]);
                    result.TryAdd(key, value);
                }
            }
            else
            {
                if (!sortOrder.Contains('-'))
                    throw new FormatException("Sort condition should be follow by format: Column1-ASC,Column2-DESC...");

                var property = sortOrder.Trim().Split("-");
                result.Add(property[0], ConvertStringToSortOrder(property[1]));
            }
        }

        return result;
    }

}
