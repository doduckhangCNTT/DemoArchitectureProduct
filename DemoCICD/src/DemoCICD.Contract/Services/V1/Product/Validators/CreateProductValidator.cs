using FluentValidation;

namespace DemoCICD.Contract.Services.V1.Product.Validators;

/// <summary>
/// Lớp validate tạo sản phẩm.
/// </summary>
public class CreateProductValidator : AbstractValidator<Command.CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.Description).NotEmpty();
    }
}
