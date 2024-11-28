using AutoMapper;
using DemoCICD.Contract.Abstractions.Message;
using DemoCICD.Contract.Services.V2.Product;
using DemoCICD.Contract.Shared;
using DemoCICD.Domain.Abstractions.Dapper;
using DemoCICD.Domain.Exceptions;

namespace DemoCICD.Application.UserCases.V2.Queries.Product;
public class GetProductByIdQueryHandler : IQueryHandler<Query.GetProductByIdQuery, Response.ProductResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Response.ProductResponse>> Handle(Query.GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(request.Id)
            ?? throw new ProductException.ProductNotFoundException(request.Id);

        var result = _mapper.Map<Response.ProductResponse>(product);

        return Result.Success(result);
    }
}
