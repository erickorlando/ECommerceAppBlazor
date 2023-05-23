using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Entities;

[Index("CategoriaId", Name = "IX_Producto_CategoriaId")]
public partial class Producto : EntityBase
{
    [StringLength(20)]
    public string CodigoSku { get; set; } = null!;

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    public int CategoriaId { get; set; }

    [Column(TypeName = "decimal(11, 2)")]
    public decimal PrecioUnitario { get; set; }

    public string? UrlImagen { get; set; }

    public string? Comentarios { get; set; }

    [ForeignKey("CategoriaId")]
    [InverseProperty("Producto")]
    public virtual Categoria Categoria { get; set; } = null!;
}
