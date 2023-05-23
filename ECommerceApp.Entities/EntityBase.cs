using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Entities;

public class EntityBase
{
    [Key]
    public int Id { get; set; }

    public bool Estado { get; set; }

    protected EntityBase()
    {
        Estado = true;
    }
}