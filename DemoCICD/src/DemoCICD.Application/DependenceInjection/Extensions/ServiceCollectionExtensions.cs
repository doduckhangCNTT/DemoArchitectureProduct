using DemoCICD.Application.Behaviors;
using DemoCICD.Application.Mapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DemoCICD.Application.DependenceInjection.Extensions;

/// <summary>
/// Chứa các cấu hình - DependenceInjection.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigureMediatR(this IServiceCollection services)
        => services
        // -- MediatR
        .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly))

        //// -- Pipeline xử lí Validate Default trước khi vào handler
        //.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationDefaultBehavior<,>))

        // -- Pipeline xử lí Validate trước khi vào handler
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))

        //// -- Pipeline xử lí Performence trước khi vào handler
        //.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformancePipelineBehavior<,>))

        //// -- Pipeline xử lí Tracing trước khi vào handler
        //.AddTransient(typeof(IPipelineBehavior<,>), typeof(TracingPipelineBehavior<,>))

        // -- Pipeline xử lí Transaction trước khi vào handler
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehavior<,>))

        // -- Fluent
        .AddValidatorsFromAssembly(Contract.AssemblyReference.Assembly, includeInternalTypes: true);

    /// <summary>
    /// Cấu hình AutoMapper.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddConfigurationAutoMapper(this IServiceCollection services)
    {
        return services.AddAutoMapper(typeof(ServiceProfile));
    }
}
