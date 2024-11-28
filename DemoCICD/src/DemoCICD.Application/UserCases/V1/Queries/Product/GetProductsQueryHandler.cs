using System.Linq.Expressions;
using AutoMapper;
using DemoCICD.Contract.Abstractions.Message;
using DemoCICD.Contract.Enumerations;
using DemoCICD.Contract.Services.V1.Product;
using DemoCICD.Contract.Shared;
using DemoCICD.Domain.Abstractions.Repositories;
using DemoCICD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoCICD.Application.UserCases.V1.Queries.Product;

/// <summary>
/// Lớp xử lí product.
/// </summary>
public class GetProductsQueryHandler : IQueryHandler<Query.GetProductsQuery, List<Response.ProductResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepository;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IRepositoryBase<Domain.Entities.Product, Guid> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<Response.ProductResponse>>> Handle(Query.GetProductsQuery request, CancellationToken cancellationToken)
    {
        //var products = await _productRepository.FindAll().ToListAsync();

        // Thực hiện search
        var productsQuery = string.IsNullOrWhiteSpace(request.SearchTerm) ?
            _productRepository.FindAll() :
            _productRepository.FindAll(x => x.Name.Contains(request.SearchTerm) || x.Description.Contains(request.SearchTerm));

        // Sắp xếp theo cột
        Expression<Func<Domain.Entities.Product, object>> keySelector = request.SortColumn?.ToLower() switch
        {
            "name" => product => product.Name,
            "price" => product => product.Price,
            "description" => product => product.Description,
            _ => product => product.Id
            //_ => product => product.CreatedDate
        };

        productsQuery = request.SortOrder == SortOrder.Descending ?
            productsQuery.OrderByDescending(keySelector) :
            productsQuery.OrderBy(keySelector);

        var products = await productsQuery.ToListAsync();
        var result = _mapper.Map<List<Response.ProductResponse>>(products);
        return result;
    }
}
