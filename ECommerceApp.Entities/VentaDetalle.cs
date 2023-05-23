using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Entities;

public partial class VentaDetalle : EntityBase
{

    public int IdVenta { get; set; }

    public int IdProducto { get; set; }

    public int Correlativo { get; set; }

    [Column(TypeName = "decimal(11, 2)")]
    public decimal PrecioUnitario { get; set; }

    [Column(TypeName = "decimal(11, 2)")]
    public decimal Cantidad { get; set; }

    [Column(TypeName = "decimal(11, 2)")]
    public decimal Total { get; set; }

    [ForeignKey("IdProducto")]
    public virtual Producto IdProductoNavigation { get; set; } = null!;

    [ForeignKey("IdVenta")]
    public virtual Venta IdVentaNavigation { get; set; } = null!;
}
