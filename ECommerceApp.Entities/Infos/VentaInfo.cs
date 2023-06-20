namespace ECommerceApp.Entities.Infos;

public class VentaInfo
{
    public int VentaId { get; set; }
    public string Cliente { get; set; } = null!;
    public string FechaVenta { get; set; } = null!;
    public string NroFactura { get; set; } = null!;
    public int CantidadProductos { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Impuestos { get; set; }
    public decimal Total { get; set; }
}