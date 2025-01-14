using MediatR;
using FluentResults;
using FluentValidation;
using vsa_journey.Application.Common.PaginatedResult;
using vsa_journey.Features.Category.GetAllCategories.Repository;

namespace vsa_journey.Features.Category.GetAllCategories.Query;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<PaginatedResult<Domain.Entities.Category.Category>>>
{
    
    private readonly IValidator<GetAllCategoriesQuery> _validator;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<GetAllCategoriesQueryHandler> _logger;


    public GetAllCategoriesQueryHandler(IValidator<GetAllCategoriesQuery> validator, ICategoryRepository categoryRepository, ILogger<GetAllCategoriesQueryHandler> logger)
    {
        _validator = validator;
        _categoryRepository = categoryRepository;
        _logger = logger;
    }

    public async Task<Result<PaginatedResult<Domain.Entities.Category.Category>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        var allCategories = await _categoryRepository.GetAllAsync(request.Skip, request.Take);

        return Result.Ok(allCategories).WithSuccess("Categories fetched successfully");
    }
}