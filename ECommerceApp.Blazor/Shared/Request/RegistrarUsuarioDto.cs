using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Blazor.Shared.Request;

public class RegistrarUsuarioDto
{
    [Required]
    [StringLength(100)]
    public string Nombres { get; set; } = null!;
    [Required]
    [StringLength(100)]
    public string Apellidos { get; set; } = null!;

    [EmailAddress]
    public string Email { get; set; } = null!;

    [StringLength(20)]
    public string Telefono { get; set; } = null!;

    public string Clave { get; set; } = null!;
    
    [Compare(nameof(Clave))]
    public string ConfirmarClave { get; set; } = null!;
}