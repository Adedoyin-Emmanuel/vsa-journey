using FluentValidation;

namespace vsa_journey.Features.Products.UpdateProduct.Command;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.Name).MaximumLength(20);
        RuleFor(command => command.Description).MaximumLength(1500);
        RuleFor(command => command.Price).GreaterThanOrEqualTo(0).When(command => command.Price.HasValue);
        RuleFor(command => command.Tags)
            .Must(tags => tags == null || tags.Any()).WithMessage("Tags must not be empty if provided."); 
        RuleFor(command => command.Quantity)
            .GreaterThanOrEqualTo(0).When(command => command.Quantity.HasValue);
        RuleFor(command => command.Files)
            .Must(files => files == null || files.Any()).WithMessage("Files must not be empty if provided.");
    }
}