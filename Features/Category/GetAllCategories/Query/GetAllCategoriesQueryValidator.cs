using FluentValidation;

namespace vsa_journey.Features.Category.GetAllCategories.Query;

public class GetAllCategoriesQueryValidator : AbstractValidator<GetAllCategoriesQuery>
{
    public GetAllCategoriesQueryValidator()
    {
        RuleFor(query => query.Take).GreaterThan(0).LessThanOrEqualTo(100);
    }
}