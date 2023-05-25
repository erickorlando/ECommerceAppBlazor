using ECommerceApp.DataAccess;
using ECommerceApp.Entities;
using ECommerceApp.Entities.Infos;
using ECommerceApp.Repositories.Interfaces;

namespace ECommerceApp.Repositories.Implementations;

public class ProductoRepository : RepositoryBase<Producto>, IProductoRepository
{
    public ProductoRepository(ECommerceDbContext context) 
        : base(context)
    {

    }

    public async Task<(ICollection<ProductoInfo> Collection, int Total)> ListAsync(string? filter, int page, int rows)
    {
        return await base.ListAsync(p => p.Estado && p.Nombre.Contains(filter ?? string.Empty),
            p => new ProductoInfo
            {
                Id = p.Id,
                Nombre = p.Nombre,
                CodigoSku = p.CodigoSku,
                PrecioUnitario = p.PrecioUnitario,
                Comentarios = p.Comentarios,
                CategoriaId = p.CategoriaId,
                Categoria = p.Categoria.NombreCategoria,
                UrlImagen = p.UrlImagen
            }, x => x.CodigoSku, page, rows);
    }
}