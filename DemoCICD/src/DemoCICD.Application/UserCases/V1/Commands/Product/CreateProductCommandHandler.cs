using DemoCICD.Contract.Abstractions.Message;
using DemoCICD.Contract.Shared;
using DemoCICD.Domain.Abstractions.Repositories;
using DemoCICD.Domain.Abstractions;
using DemoCICD.Persistence;
using MediatR;
using DemoCICD.Application.Exceptions;
using DemoCICD.Contract.Services.V1.Product;

namespace DemoCICD.Application.UserCases.V1.Commands.Product;
public sealed class CreateProductCommandHandler : ICommandHandler<Command.CreateProductCommand>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepository;
    private readonly ApplicationDbContext _context; // SQL-SERVER-STRATEGY-1 (Chiến lược này sẽ phá vỡ nguyên tắc Clean Architecture)
    private readonly IUnitOfWork _unitOfWork; // SQL-SERVER-STRATEGY-2 (Chiến lược này chỉ sử dụng được khi cấu hình database không sử dụng ExecutionStrategy)
    private readonly IPublisher _publisher;

    public CreateProductCommandHandler(IRepositoryBase<Domain.Entities.Product, Guid> productRepository,
        IUnitOfWork unitOfWork,
        ApplicationDbContext context,
        IPublisher publisher)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _context = context;
        _publisher = publisher;
    }

    public async Task<Result> Handle(Command.CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Cách trả ra nhiều lỗi
        //ValidationError[] errors = new ValidationError[]
        //{
        //    new ValidationError("abc123", "Lỗi 1"),
        //    new ValidationError("bcd234", "Lỗi 2")
        //};

        //throw new ValidationException(errors);

        //var product = new Domain.Entities.Product()
        //{
        //    Id = Guid.NewGuid(),
        //    Name = request.Name,
        //    Description = request.Description,
        //    Price = request.Price
        //};
        var product = Domain.Entities.Product.CreateProduct(Guid.NewGuid(), request.Name, request.Price, request.Description);

        _productRepository.Add(product);
        // await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _context.SaveChangesAsync();

        var productCreated = await _productRepository.FindByIdAsync(product.Id);

        //var productSecond = new Domain.Entities.Product()
        //{
        //    Id = Guid.NewGuid(),
        //    Name = productCreated.Name,
        //    Description = productCreated.Id.ToString(),
        //    Price = productCreated.Price  
        //};
        //_productRepository.Add(product);
        // await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _context.SaveChangesAsync();

        // => Send Email
        //await _publisher.Publish(new DomainEvent.ProductCreated(productCreated.Id), cancellationToken);
        //await _publisher.Publish(new DomainEvent.ProductDelete(productCreated.Id), cancellationToken);

        await Task.WhenAll(
            _publisher.Publish(new DomainEvent.ProductCreated(productCreated.Id), cancellationToken),
            _publisher.Publish(new DomainEvent.ProductDelete(productCreated.Id), cancellationToken));

        return Result.Success();
    }
}
