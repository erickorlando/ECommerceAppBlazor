using ECommerceApp.Blazor.Shared.Response;

namespace ECommerceApp.Blazor.Shared;

public class CarritoDto
{
    public ProductoDto ProductoDto { get; set; } = null!;
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public decimal Total { get; set; }
}