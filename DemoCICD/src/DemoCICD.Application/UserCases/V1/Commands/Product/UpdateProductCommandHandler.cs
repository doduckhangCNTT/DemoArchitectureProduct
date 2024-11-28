using DemoCICD.Contract.Abstractions.Message;
using DemoCICD.Contract.Services.V1.Product;
using DemoCICD.Contract.Shared;

namespace DemoCICD.Application.UserCases.V1.Commands.Product;
public sealed class UpdateProductCommandHandler : ICommandHandler<Command.UpdateProductCommand>
{
    public Task<Result> Handle(Command.UpdateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
