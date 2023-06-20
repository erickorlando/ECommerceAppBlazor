using ECommerceApp.DataAccess;
using ECommerceApp.Entities;
using ECommerceApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Repositories.Implementations;

public class VentaRepository : RepositoryBase<Venta>, IVentaRepository
{
    public VentaRepository(ECommerceDbContext context) : base(context)
    {

    }

    public async Task CrearTransaccionAsync()
    {
        await Context.Database.BeginTransactionAsync();
    }

    public async Task RollBackAsync()
    {
        await Context.Database.RollbackTransactionAsync();
    }

    public override async Task UpdateAsync()
    {
        await base.UpdateAsync();
        await Context.Database.CommitTransactionAsync();
    }

    public override async Task<int> AddAsync(Venta entity)
    {
        entity.FechaVenta = DateTime.Now;
        var lastNumber = await Context.Set<Venta>().CountAsync() + 1;
        entity.NroDocumento = $"FAC-{lastNumber:00000000}";

        await Context.Set<Venta>().AddAsync(entity);

        return entity.Id;
    }

    public async Task AddVentaDetalleAsync(VentaDetalle detalle)
    {
        await Context.Set<VentaDetalle>().AddAsync(detalle);
    }
}