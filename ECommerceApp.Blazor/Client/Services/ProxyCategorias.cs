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
        var response = await _httpClient.GetFromJsonAsync<ICollection<CategoriaDto>>("api/Categorias");
        if (response != null)
            return response;

        return new List<CategoriaDto>();
    }
}