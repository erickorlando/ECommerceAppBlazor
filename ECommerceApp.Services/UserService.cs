using System.IdentityModel.Tokens.Jwt;
using System.Security;
using ECommerceApp.Blazor.Shared;
using ECommerceApp.Blazor.Shared.Request;
using ECommerceApp.Blazor.Shared.Response;
using ECommerceApp.DataAccess;
using ECommerceApp.Entities;
using ECommerceApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Security.Claims;
using ECommerceApp.Entities.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ECommerceApp.Services;

public class UserService : IUserService
{
    private readonly UserManager<IdentityUserECommerce> _userManager;
    private readonly ILogger<UserService> _logger;
    private readonly IClienteRepository _clienteRepository;
    private readonly Jwt _jwt;

    public UserService(UserManager<IdentityUserECommerce> userManager,
        ILogger<UserService> logger,
        IClienteRepository clienteRepository,
        IOptions<AppConfig> options)
    {
        _userManager = userManager;
        _logger = logger;
        _clienteRepository = clienteRepository;
        _jwt = options.Value.Jwt;
    }

    public async Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request)
    {
        var response = new LoginDtoResponse();
        try
        {
            var identity = await _userManager.FindByEmailAsync(request.UserName);

            if (identity is null)
                throw new SecurityException("Usuario no existe");

            if (await _userManager.IsLockedOutAsync(identity))
                throw new SecurityException($"Demasiados intentos fallidos para el usuario {identity.Nombres}");

            var result = await _userManager.CheckPasswordAsync(identity, request.Password);
            if (!result)
            {
                await _userManager.AccessFailedAsync(identity);
                throw new SecurityException($"Clave incorrecta para el usuario {identity.UserName}");
            }

            var roles = await _userManager.GetRolesAsync(identity);

            var fechaExpiracion = DateTime.Now.AddHours(1);

            // Vamos a crear los claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, identity.Nombres),
                new Claim(ClaimTypes.Surname, identity.Apellidos),
                new Claim(ClaimTypes.MobilePhone, identity.PhoneNumber ?? string.Empty),
                new Claim(ClaimTypes.Email, request.UserName),
                new Claim(ClaimTypes.Expiration, fechaExpiracion.ToString("yyyy-MM-dd HH:mm:ss"))
            };

            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));
            response.Roles = new List<string>();
            response.Roles.AddRange(roles);

            // Creacion del JWT
            var llaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SecretKey));
            var credenciales = new SigningCredentials(llaveSimetrica, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(credenciales);

            var payload = new JwtPayload(
                issuer: _jwt.Emisor,
                audience: _jwt.Audiencia,
                notBefore: DateTime.Now,
                claims: claims,
                expires: fechaExpiracion);

            var token = new JwtSecurityToken(header, payload);
            response.Token = new JwtSecurityTokenHandler().WriteToken(token);
            response.FullName = $"{identity.Nombres} {identity.Apellidos}";
            response.Success = true;

            _logger.LogInformation("Se creó el JWT de forma satisfactoria");
        }
        catch (SecurityException ex)
        {
            response.ErrorMessage = ex.Message;
            _logger.LogWarning(ex, "Error de seguridad {Message}", ex.Message);
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error de autenticacion";
            _logger.LogCritical(ex, "Error de autenticacion {Message}", ex.Message);
        }
        return response;
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
                    await _userManager.AddToRoleAsync(userIdentity, Constantes.RolUsuario);

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