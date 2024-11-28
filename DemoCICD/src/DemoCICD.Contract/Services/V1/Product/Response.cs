namespace DemoCICD.Contract.Services.V1.Product;

/// <summary>
/// Gía trị phản hồi của query
/// </summary>
public static class Response
{
    public record ProductResponse(Guid Id, string Name, decimal Price, string Description);
}
