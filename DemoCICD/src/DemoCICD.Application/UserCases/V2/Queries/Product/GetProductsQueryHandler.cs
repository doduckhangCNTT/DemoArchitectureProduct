using AutoMapper;
using DemoCICD.Contract.Abstractions.Message;
using DemoCICD.Contract.Services.V2.Product;
using DemoCICD.Contract.Shared;
using DemoCICD.Domain.Abstractions.Dapper;
using DemoCICD.Domain.Abstractions.Repositories;
using MediatR;

namespace DemoCICD.Application.UserCases.V2.Queries.Product;

/// <summary>
/// Lớp xử lí product.
/// </summary>
public class GetProductsQueryHandler : IQueryHandler<Query.GetProductsQuery, Result<List<Response.ProductResponse>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetProductsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    async Task<Result<Result<List<Response.ProductResponse>>>> IRequestHandler<Query.GetProductsQuery, Result<Result<List<Response.ProductResponse>>>>.Handle(Query.GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.GetAllAsync();
        var results = _mapper.Map<List<Response.ProductResponse>>(products);

        return Result.Success(results);
    }
}
