using Dapper;
using ECommerceApp.DataAccess;
using ECommerceApp.Entities;
using ECommerceApp.Entities.Infos;
using ECommerceApp.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
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

    public async Task<(ICollection<VentaInfo> Collection, int Total)> ListarVentas(DateTime fechaInicio, DateTime fechaFin, int pagina, int cantidad)
    {
        var connection = new SqlConnection(Context.Database.GetConnectionString());

        var query = connection.Query<VentaInfo>("EXEC dbo.uspListarVentas @fechaInicio, @fechaFin, @pagina, @filas", 
            new { fechaInicio, fechaFin, pagina = ((pagina - 1) * cantidad), filas = cantidad});

        var total = connection.QuerySingle<int>(@"SELECT COUNT(*) FROM Venta 
                    WHERE CAST(FechaVenta AS DATE) BETWEEN @fechaInicio AND @fechaFin", new { fechaInicio, fechaFin });

        return await Task.FromResult((query.ToList(), total));
    }

    public async Task<DashboardInfo> GetDashboardInfoAsync(DateTime fechaInicio, DateTime fechafin)
    {
        var connection = new SqlConnection(Context.Database.GetConnectionString());

        var query = connection.QuerySingle<DashboardInfo>("EXEC dbo.uspDashboard @fechaInicio, @fechaFin",
            new { fechaInicio, fechafin });

        return await Task.FromResult(query);
    }
}