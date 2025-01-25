using FluentValidation;

namespace vsa_journey.Features.Products.UpdateProduct.Command;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.Name).NotEmpty().MaximumLength(20);
        RuleFor(command => command.Description).NotEmpty().MaximumLength(1500);
        RuleFor(command => command.Price).NotEmpty();
        RuleFor(command => command.Tags).NotEmpty();
        RuleFor(command => command.Quantity).NotEmpty();
        RuleFor(command => command.Files).NotEmpty();
    }
}