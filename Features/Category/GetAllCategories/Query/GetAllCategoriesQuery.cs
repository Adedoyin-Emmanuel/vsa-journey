using FluentResults;
using MediatR;

namespace vsa_journey.Features.Category.GetAllCategories.Query;

public sealed record GetAllCategoriesQuery : IRequest<Result<object>>
{
    public int Take { get; set; } = 10;
    public int Skip { get; set; } = 0;
}