using DemoCICD.Domain.Abstractions.Dapper;
using DemoCICD.Domain.Abstractions.Dapper.Repositories.Product;
using DemoCICD.Infrastructure.Dapper.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DemoCICD.Infrastructure.Dapper.DependenceInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureDapper(this IServiceCollection services)
    => services.AddTransient<IProductRepository, ProductRepository>()
        .AddTransient<IUnitOfWork, UnitOfWork>();
}
