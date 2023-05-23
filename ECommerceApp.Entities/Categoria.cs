using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Entities;

public partial class Categoria : EntityBase
{
    [StringLength(100)]
    public string NombreCategoria { get; set; } = null!;

    [InverseProperty("Categoria")]
    public virtual ICollection<Producto> Producto { get; set; } = new List<Producto>();
}
