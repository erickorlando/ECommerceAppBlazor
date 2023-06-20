using ECommerceApp.Entities;
using ECommerceApp.Entities.Infos;

namespace ECommerceApp.Repositories.Interfaces;

public interface IProductoRepository : IRepositoryBase<Producto>
{
    Task<(ICollection<ProductoInfo> Collection, int Total)> ListAsync(string? filter, int page, int rows);

    Task<Dictionary<int, decimal>> GetPreciosAsync(IEnumerable<int> ids);
}