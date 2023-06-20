using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Entities;

public partial class Venta : EntityBase
{
    public int IdCliente { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaVenta { get; set; }

    [StringLength(50)]
    public string NroDocumento { get; set; } = null!;

    [StringLength(500)]
    public string? Glosa { get; set; }

    [Column(TypeName = "decimal(11, 2)")]
    public decimal SubTotal { get; set; }

    [Column(TypeName = "decimal(11, 2)")]
    public decimal Impuestos { get; set; }

    [Column(TypeName = "decimal(11, 2)")]
    public decimal Total { get; set; }

    public int EstadoDocumento { get; set; }

    [ForeignKey("IdCliente")]
    [InverseProperty("Venta")]
    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
