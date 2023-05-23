namespace ECommerceApp.Blazor.Client.Shared;

public class PaginationDataBase
{
    public int CurrentPage { get; set; }
    public int RowsPerPage { get; set; }
    public int TotalPages { get; set; }
    public int RowCount { get; set; }
}

public class PagedResult<T> : PaginationDataBase
{
    public ICollection<T> Results { get; set; }

    public PagedResult()
    {
        Results = new List<T>();
    }
}