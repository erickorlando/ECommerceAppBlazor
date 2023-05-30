using ECommerceApp.Entities;

namespace ECommerceApp.Repositories.Interfaces;

public interface IClienteRepository : IRepositoryBase<Cliente>
{
    Task<Cliente?> FindByEmailAsync(string email);
}