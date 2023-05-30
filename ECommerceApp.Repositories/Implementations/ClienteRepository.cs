using ECommerceApp.DataAccess;
using ECommerceApp.Entities;
using ECommerceApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Repositories.Implementations;

public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
{
    public ClienteRepository(ECommerceDbContext context) 
        : base(context)
    {
    }

    public async Task<Cliente?> FindByEmailAsync(string email)
    {
        return await Context.Set<Cliente>()
            .FirstOrDefaultAsync(p => p.CorreoElectronico == email);
    }
}