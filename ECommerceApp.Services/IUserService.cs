using ECommerceApp.Blazor.Shared.Request;
using ECommerceApp.Blazor.Shared.Response;

namespace ECommerceApp.Services;

public interface IUserService
{
    Task<BaseResponse> RegisterAsync(RegistrarUsuarioDto request);
}