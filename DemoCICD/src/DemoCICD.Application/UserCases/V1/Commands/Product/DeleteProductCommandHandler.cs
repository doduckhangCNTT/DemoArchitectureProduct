using DemoCICD.Contract.Abstractions.Message;
using DemoCICD.Contract.Services.V1.Product;
using DemoCICD.Contract.Shared;
using DemoCICD.Domain.Abstractions;
using DemoCICD.Domain.Abstractions.Repositories;
using DemoCICD.Domain.Exceptions;
using DemoCICD.Persistence;
using MediatR;
using static DemoCICD.Contract.Services.V1.Product.DomainEvent;

namespace DemoCICD.Application.UserCases.V1.Commands.Product;
public sealed class DeleteProductCommandHandler : ICommandHandler<Command.DeleteProductCommand>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepository;
    private readonly ApplicationDbContext _context; // SQL-SERVER-STRATEGY-1
    private readonly IUnitOfWork _unitOfWork; // SQL-SERVER-STRATEGY-2
    private readonly IPublisher _publisher;

    public DeleteProductCommandHandler(IRepositoryBase<Domain.Entities.Product, Guid> productRepository, ApplicationDbContext context, IUnitOfWork unitOfWork, IPublisher publisher)
    {
        _productRepository = productRepository;
        _context = context;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result> Handle(Command.DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(request.Id) ?? throw new ProductException.ProductNotFoundException(request.Id);

        _productRepository.Remove(product);

        // => Send Email
        await _publisher.Publish(new DomainEvent.ProductDelete(product.Id), cancellationToken);
        return Result.Success();
    }
}
