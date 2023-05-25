namespace ECommerceApp.Entities.Infos;

public class ProductoInfo
{
    public int Id { get; set; }
    public string CodigoSku { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public string Categoria { get; set; } = null!;
    public int CategoriaId { get; set; }
    public decimal PrecioUnitario { get; set; }
    public string? Comentarios { get; set; }
    public string? UrlImagen { get; set; }
}