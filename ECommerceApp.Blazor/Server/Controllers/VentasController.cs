using System.Security.Claims;
using ECommerceApp.Blazor.Shared.Request;
using ECommerceApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Blazor.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VentasController : ControllerBase
{
    private readonly IVentaService _service;

    public VentasController(IVentaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Post(SaleDtoRequest request)
    {
        // primero obtenemos el email del usuario autenticado
        var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        return Ok(await _service.AddAsync(email!, request));
    }
}