using DemoCICD.Contract.Services.V2.Product;
using FluentValidation;

namespace DemoCICD.Contract.Services.V2.Product.Validators;

/// <summary>
/// Validate xóa sản phẩm 
/// </summary>
public class DeleteProductValidator : AbstractValidator<Command.DeleteProductCommand>
{
    public DeleteProductValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
