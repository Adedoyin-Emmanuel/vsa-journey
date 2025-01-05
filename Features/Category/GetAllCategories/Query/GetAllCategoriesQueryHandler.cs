using FluentResults;
using FluentValidation;
using MediatR;
using vsa_journey.Features.Category.GetAllCategories.Repository;

namespace vsa_journey.Features.Category.GetAllCategories.Query;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<object>>
{
    
    private readonly IValidator<GetAllCategoriesQuery> _validator;
    private readonly ICategoryRepository _repository;
    private readonly ILogger<GetAllCategoriesQueryHandler> _logger;


    public GetAllCategoriesQueryHandler(IValidator<GetAllCategoriesQuery> validator, ICategoryRepository repository, ILogger<GetAllCategoriesQueryHandler> logger)
    {
        _validator = validator;
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<object>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}