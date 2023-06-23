namespace ECommerceApp.Blazor.Shared.Response;

public class DashboardDtoResponse
{
    public int CantidadVentas { get; set; }
    public int CantidadClientes { get; set; }
    public int CantidadProductos { get; set; }
    public int CantidadCategorias { get; set; }
    public decimal SumaTotal { get; set; }
    public decimal PromedioVenta { get; set; }
}