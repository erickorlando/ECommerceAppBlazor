using ECommerceApp.Entities;
using ECommerceApp.Entities.Infos;

namespace ECommerceApp.Repositories.Interfaces;

public interface IVentaRepository : IRepositoryBase<Venta>
{
    Task CrearTransaccionAsync();
    Task RollBackAsync();
    Task AddVentaDetalleAsync(VentaDetalle detalle);
    Task<(ICollection<VentaInfo> Collection, int Total)> ListarVentas(DateTime fechaInicio, DateTime fechaFin, int pagina, int cantidad);
    Task<DashboardInfo> GetDashboardInfoAsync(DateTime fechaInicio, DateTime fechafin);
}