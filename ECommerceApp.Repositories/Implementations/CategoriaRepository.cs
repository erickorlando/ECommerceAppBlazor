using ECommerceApp.DataAccess;
using ECommerceApp.Entities;
using ECommerceApp.Repositories.Interfaces;

namespace ECommerceApp.Repositories.Implementations;

public class CategoriaRepository : RepositoryBase<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(ECommerceDbContext context) 
        : base(context)
    {

    }
}