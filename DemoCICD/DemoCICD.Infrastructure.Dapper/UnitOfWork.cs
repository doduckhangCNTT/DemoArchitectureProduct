using DemoCICD.Domain.Abstractions.Dapper;
using DemoCICD.Domain.Abstractions.Dapper.Repositories.Product;

namespace DemoCICD.Infrastructure.Dapper;
public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IProductRepository productRepository)
    {
        Products = productRepository;
    }

    public IProductRepository Products { get; }
}
