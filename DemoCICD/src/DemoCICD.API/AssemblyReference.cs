using System.Reflection;

namespace DemoCICD.API;

/// <summary>
/// Lấy tất cả các reference trong namespace (DemoCICD.API)
/// Mục đích để kiểm tra xem namespace (DemoCICD.API) có using đến các namespace khác hay không (kiểu để quản lí các tham chiếu đến các namespace Vd: Application chỉ đc phép tham chiếu đến Domain)
/// </summary>
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
