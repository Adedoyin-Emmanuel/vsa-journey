using MediatR;
using FluentResults;
using vsa_journey.Application.Common.PaginatedResult;

namespace vsa_journey.Features.Category.GetAllCategories.Query;

public sealed record GetAllCategoriesQuery : IRequest<Result<PaginatedResult<Domain.Entities.Category.Category>>>
{
    public int Take { get; set; } = 10;
    public int Skip { get; set; } = 0;
}