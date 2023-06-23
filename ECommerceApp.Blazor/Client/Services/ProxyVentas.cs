using System.Net.Http.Json;
using ECommerceApp.Blazor.Shared.Request;
using ECommerceApp.Blazor.Shared.Response;

namespace ECommerceApp.Blazor.Client.Services;

public class ProxyVentas
{
    private readonly HttpClient _httpClient;

    public ProxyVentas(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<BaseResponse> Registrar(SaleDtoRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Ventas", request);
        var result = await response.Content.ReadFromJsonAsync<BaseResponse>();
        if (result!.Success is false)
        {
            throw new InvalidOperationException(result.ErrorMessage);
        }

        return result;
    }

    public async Task<DashboardDtoResponse> GetDashboard(string fechaInicio, string fechaFin)
    {
        var response = await _httpClient
            .GetFromJsonAsync<BaseResponseGeneric<DashboardDtoResponse>>($"api/Ventas/dashboard?fechaInicio={fechaInicio}&fechaFin={fechaFin}");
        if (response!.Success)
        {
            return response.Data!;
        }

        throw new InvalidOperationException(response.ErrorMessage);
    }
}