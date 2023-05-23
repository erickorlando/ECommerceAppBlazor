namespace ECommerceApp.Blazor.Shared.Response;

public class PaginationResponse<T>
{
    public ICollection<T>? Data { get; set; }
    public int TotalPages { get; set; }
    public bool Success { get; set; }
}