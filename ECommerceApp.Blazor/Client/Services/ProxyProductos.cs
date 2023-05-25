using ECommerceApp.Blazor.Shared.Request;
using ECommerceApp.Blazor.Shared.Response;
using System.Net.Http.Json;

namespace ECommerceApp.Blazor.Client.Services;

public class ProxyProductos
{
    private readonly HttpClient _httpClient;

    public ProxyProductos(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PaginationResponse<ProductoDto>> ListarProductos(string? filter, int page=1, int rows = 5)
    {
        var response = await _httpClient.GetFromJsonAsync<PaginationResponse<ProductoDto>>($"api/Productos?filter={filter ?? string.Empty}&page={page}&rows={rows}");
        if (response != null)
        {
            return response;
        }

        return new PaginationResponse<ProductoDto>();
    }

    public async Task<ProductoDtoRequest> GetProducto(int id)
    {
        var response = await _httpClient.GetFromJsonAsync<BaseResponseGeneric<ProductoDtoRequest>>($"api/Productos/{id}");
        if (response!.Success)
        {
            return response.Data!;
        }

        throw new InvalidOperationException("No se encontro el registro");
    }

    public async Task CrearProducto(ProductoDtoRequest request)
    {
        await _httpClient.PostAsJsonAsync("api/Productos", request);
    }

    public async Task ActualizarProducto(int id, ProductoDtoRequest request)
    {
        await _httpClient.PutAsJsonAsync($"api/Productos/{id}", request);
    }

    public async Task ElminarProducto(int id)
    {
        await _httpClient.DeleteAsync($"api/Productos/{id}");
    }
}