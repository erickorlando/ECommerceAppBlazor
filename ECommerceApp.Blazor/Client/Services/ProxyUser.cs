using System.Net.Http.Json;
using System.Security;
using ECommerceApp.Blazor.Shared.Request;
using ECommerceApp.Blazor.Shared.Response;

namespace ECommerceApp.Blazor.Client.Services;

public class ProxyUser
{
    private readonly HttpClient _httpClient;

    public ProxyUser(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LoginDtoResponse> Login(LoginDtoRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Users/Login", request);
        var loginResponse = await response.Content.ReadFromJsonAsync<LoginDtoResponse>();

        if (loginResponse!.Success)
            return loginResponse;

        throw new SecurityException(loginResponse.ErrorMessage);
    }

    public async Task Register(RegistrarUsuarioDto request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Users/Register", request);
        if (response.IsSuccessStatusCode)
        {
            var resultado = await response.Content.ReadFromJsonAsync<BaseResponse>();
            if (resultado!.Success == false)
                throw new InvalidOperationException(resultado.ErrorMessage);
        }
        else
        {
            throw new InvalidOperationException(response.ReasonPhrase);
        }
    }

    public async Task SendTokenToResetPassword(GenerateTokenToResetDtoRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Users/SendTokenToResetPassword", request);
        if (!response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadFromJsonAsync<BaseResponse>();
            throw new InvalidOperationException(jsonResponse!.ErrorMessage);
        }
    }

    public async Task ResetPassword(ResetPasswordDtoRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Users/ResetPassword", request);
        if (!response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadFromJsonAsync<BaseResponse>();
            throw new InvalidOperationException(jsonResponse!.ErrorMessage);
        }
    }
}