using FluentValidation;
using vsa_journey.Features.Products.CreateProduct.Command;

namespace vsa_journey.Features.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(command => command.Name).NotEmpty().MaximumLength(20);
        RuleFor(command => command.Description).NotEmpty().MaximumLength(1500);
        RuleFor(command => command.Price).NotEmpty();
        RuleFor(command => command.Tags).NotEmpty();
        RuleFor(command => command.Quantity).NotEmpty().GreaterThan(0);
        RuleFor(command => command.CategoryId).NotEmpty();
        RuleFor(command => command.Files).NotEmpty();
    }
}