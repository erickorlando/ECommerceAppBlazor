using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Blazor.Shared.Request;

public class TarjetaDto
{
    [Required(ErrorMessage = "El titular es requerido")]
    public string? Titular { get; set; }
    
    [Required(ErrorMessage = "El número de tarjeta es requerido")]
    public string? Numero { get; set; }

    [Required(ErrorMessage = "La vigencia es requerida")]
    public string? Vigencia { get; set; }

    [Required(ErrorMessage = "El CVV es requerido")]
    public string? Cvv { get; set; }
}