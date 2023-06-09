﻿using ECommerceApp.Blazor.Shared.Request;
using ECommerceApp.Blazor.Shared.Response;

namespace ECommerceApp.Services;

public interface IUserService
{
    Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request);

    Task<BaseResponse> RegisterAsync(RegistrarUsuarioDto request);
    Task<BaseResponse> SendTokenToResetPasswordAsync(GenerateTokenToResetDtoRequest request);
    Task<BaseResponse> ResetPasswordAsync(ResetPasswordDtoRequest request);
}