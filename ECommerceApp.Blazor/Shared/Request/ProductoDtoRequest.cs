using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Blazor.Shared.Request;

public class ProductoDtoRequest
{
    [Required]
    public string Nombre { get; set; } = null!;

    [Required]
    public string CodigoSku { get; set; } = null!;
    public int CategoriaId { get; set; }
    public decimal PrecioUnitario { get; set; }
    public string? Comentarios { get; set; }
}