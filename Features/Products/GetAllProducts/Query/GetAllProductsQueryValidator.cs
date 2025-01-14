using FluentValidation;

namespace vsa_journey.Features.Products.GetAllProducts.Query;

public class GetAllProductsQueryValidator : AbstractValidator<GetAllProductsQuery>
{
    public GetAllProductsQueryValidator()
    {
        RuleFor(query => query.Take).GreaterThan(0).LessThanOrEqualTo(100);
    }

 
}