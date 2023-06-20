using ECommerceApp.DataAccess;
using ECommerceApp.Entities;
using ECommerceApp.Entities.Infos;
using ECommerceApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Dictionary<int, decimal>> GetPreciosAsync(IEnumerable<int> ids)
    {
        // Pasamos todos los ID recibidos y devolvemos unicamente los precios de esos productos.
        var diccionario = new Dictionary<int, decimal>();

        var query = await Context.Set<Producto>()
            .Where(p => ids.Contains(p.Id))
            .Select(p => new 
            {
                p.Id,
                p.PrecioUnitario
            })
            .ToListAsync();

        // Programacion funcional
        query.ForEach(p =>
        {
            diccionario.Add(p.Id, p.PrecioUnitario);
        });

        return diccionario;
    }
}