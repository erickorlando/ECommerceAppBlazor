using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Blazor.Shared.Request;

public class ProductoDtoRequest
{
    [Required]
    public string Nombre { get; set; } = null!;

    [Required]
    public string CodigoSku { get; set; } = null!;
    public int IdCategoria { get; set; }
    public decimal PrecioUnitario { get; set; }
}