using ECommerceApp.Entities;

namespace ECommerceApp.Repositories.Interfaces;

public interface IVentaRepository : IRepositoryBase<Venta>
{
    Task CrearTransaccionAsync();

    Task RollBackAsync();
    Task AddVentaDetalleAsync(VentaDetalle detalle);
}