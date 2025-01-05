using FluentResults;
using FluentValidation;
using MediatR;

namespace vsa_journey.Features.Category.GetAllCategories.Query;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<object>>
{
    
    private readonly IValidator<GetAllCategoriesQuery> validator;
    
    public async Task<Result<object>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}