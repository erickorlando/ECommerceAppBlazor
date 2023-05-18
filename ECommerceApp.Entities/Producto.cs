namespace ECommerceApp.Entities
{
    public class Producto : EntityBase
    {
        public string CodigoSku { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public Categoria Categoria { get; set; } = null!;
        public int CategoriaId { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}