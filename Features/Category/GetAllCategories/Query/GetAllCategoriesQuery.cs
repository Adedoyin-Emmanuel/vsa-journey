namespace vsa_journey.Features.Category.GetAllCategories.Query;

public sealed record GetAllCategoriesQuery
{
    public int Take { get; set; } = 10;
    public int Skip { get; set; } = 0;
}