using ECommerceApp.Blazor.Shared.Response;
using System.Net.Http.Json;

namespace ECommerceApp.Blazor.Client.Services;

public class ProxyCategorias
{
    private readonly HttpClient _httpClient;

    public ProxyCategorias(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ICollection<CategoriaDto>> ListarCategorias()
    {
        var response = await _httpClient.GetFromJsonAsync<BaseResponseGeneric<ICollection<CategoriaDto>>>("api/Categorias");
        if (response!.Success)
            return response.Data!;

        throw new InvalidOperationException(response.ErrorMessage);
    }

    public async Task<CategoriaDto> ObtenerCategoria(int id)
    {
        var response = await _httpClient.GetFromJsonAsync<BaseResponseGeneric<CategoriaDto>>($"api/Categorias/{id}");
        if (response!.Success)
            return response.Data!;

        throw new InvalidOperationException(response.ErrorMessage);
    }

    public async Task AgregarCategoria(CategoriaDto request)
    {
        await _httpClient.PostAsJsonAsync("api/Categorias", request);
    }

    public async Task ActualizarCategoria(int id, CategoriaDto request)
    {
        await _httpClient.PutAsJsonAsync($"api/Categorias/{id}", request);
    }

    public async Task EliminarCategoria(int id)
    {
        await _httpClient.DeleteAsync($"api/Categorias/{id}");
    }
}