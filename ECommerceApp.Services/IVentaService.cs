using ECommerceApp.Blazor.Shared.Request;
using ECommerceApp.Blazor.Shared.Response;

namespace ECommerceApp.Services;

public interface IVentaService
{
    Task<BaseResponse> AddAsync(string email, SaleDtoRequest request);

    Task<PaginationResponse<VentaDtoResponse>> ListarVentasAsync(DateTime fechaInicio, DateTime fechaFin, int page,
        int cantidad);
}