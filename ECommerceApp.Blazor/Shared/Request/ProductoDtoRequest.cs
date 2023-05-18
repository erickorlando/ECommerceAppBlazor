namespace ECommerceApp.Blazor.Shared.Request;

public class ProductoDtoRequest
{
    public string Nombre { get; set; } = null!;
    public string CodigoSku { get; set; } = null!;
    public int IdCategoria { get; set; }
    public decimal PrecioUnitario { get; set; }
}