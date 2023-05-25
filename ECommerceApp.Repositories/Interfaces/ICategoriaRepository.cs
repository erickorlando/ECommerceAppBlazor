using ECommerceApp.Entities;

namespace ECommerceApp.Repositories.Interfaces;

public interface ICategoriaRepository : IRepositoryBase<Categoria>
{
    Task<ICollection<Categoria>> ListAsync();
}