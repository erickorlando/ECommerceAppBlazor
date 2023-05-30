using System.Text;
using ECommerceApp.Blazor.Shared.Request;
using ECommerceApp.Blazor.Shared.Response;
using ECommerceApp.DataAccess;
using ECommerceApp.Entities;
using ECommerceApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerceApp.Services;

public class UserService : IUserService
{
    private readonly UserManager<IdentityUserECommerce> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<UserService> _logger;
    private readonly IClienteRepository _clienteRepository;

    public UserService(UserManager<IdentityUserECommerce> userManager,
        RoleManager<IdentityRole> roleManager,
        ILogger<UserService> logger,
        IClienteRepository clienteRepository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
        _clienteRepository = clienteRepository;
    }
    public async Task<BaseResponse> RegisterAsync(RegistrarUsuarioDto request)
    {
        var response = new BaseResponse();

        try
        {
            var user = new IdentityUserECommerce
            {
                Nombres = request.Nombres,
                Apellidos = request.Apellidos,
                Email = request.Email,
                PhoneNumber = request.Telefono,
                EmailConfirmed = true,
                UserName = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Clave);

            if (result.Succeeded)
            {
                var userIdentity = await _userManager.FindByEmailAsync(request.Email);

                if (userIdentity is not null)
                {
                    var userCount = await _userManager.Users.CountAsync();
                    if (userCount == 1)
                    {
                        var roleName = "Admin";
                        if (!await _roleManager.RoleExistsAsync(roleName))
                            await _roleManager.CreateAsync(new IdentityRole(roleName));

                        await _userManager.AddToRoleAsync(userIdentity, roleName);
                    }
                    else
                    {
                        var roleName = "Customer";
                        if (!await _roleManager.RoleExistsAsync(roleName))
                            await _roleManager.CreateAsync(new IdentityRole(roleName));

                        await _userManager.AddToRoleAsync(userIdentity, roleName);
                    }

                    var cliente = new Cliente
                    {
                        Nombres = userIdentity.Nombres,
                        Apellidos = userIdentity.Apellidos,
                        CorreoElectronico = userIdentity.Email!,
                        Edad = 18,
                        Telefono = userIdentity.PhoneNumber!,
                        Estado = true
                    };

                    await _clienteRepository.AddAsync(cliente);

                    // Mandar un correo electronico, felicitando la creacion de la cuenta.

                    response.Success = true;
                }
            }
            else
            {
                response.Success = false;
                var sb = new StringBuilder();
                foreach (var identityError in result.Errors)
                {
                    sb.AppendLine(identityError.Description);
                }

                response.ErrorMessage = sb.ToString();
                sb.Clear();
            }
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al crear el usuario";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }
}