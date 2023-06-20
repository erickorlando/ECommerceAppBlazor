namespace ECommerceApp.Blazor.Shared.Request;

public record SaleDtoRequest(ICollection<SaleDetailDto> Details);

public record SaleDetailDto(int ProductoId, decimal Cantidad);