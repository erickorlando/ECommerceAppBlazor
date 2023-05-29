using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Blazor.Shared.Response;

public class CategoriaDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    public string Nombre { get; set; } = null!;
}