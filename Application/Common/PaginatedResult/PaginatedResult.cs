namespace vsa_journey.Application.Common.PaginatedResult;

public class PaginatedResult<T>
{
    public List<T> Items { get; set; } = [];
    
    public int Total { get; set; }
    
    public int Skip { get; set; }
    
    public int Take { get; set; }
    
}