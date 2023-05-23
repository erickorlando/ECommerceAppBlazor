using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Entities;

public partial class Cliente : EntityBase
{
    [StringLength(150)]
    public string Nombres { get; set; } = null!;

    [StringLength(150)]
    public string Apellidos { get; set; } = null!;

    [StringLength(500)]
    public string CorreoElectronico { get; set; } = null!;

    public int Edad { get; set; }

    [StringLength(50)]
    public string Telefono { get; set; } = null!;

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
