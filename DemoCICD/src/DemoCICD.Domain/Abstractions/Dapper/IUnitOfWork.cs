using DemoCICD.Domain.Abstractions.Dapper.Repositories.Product;

namespace DemoCICD.Domain.Abstractions.Dapper;
public interface IUnitOfWork
{
    IProductRepository Products { get; }
}
