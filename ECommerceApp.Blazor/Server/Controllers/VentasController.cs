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
    private readonly ILogger<VentasController> _logger;

    public VentasController(IVentaService service, ILogger<VentasController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Post(SaleDtoRequest request)
    {
        // primero obtenemos el email del usuario autenticado
        var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        return Ok(await _service.AddAsync(email!, request));
    }

    [HttpGet]
    public async Task<IActionResult> Get(string fechaInicio, string fechaFin, int page = 1, int filas = 10)
    {
        try
        {
            var fin = DateTime.Parse(fechaFin).AddDays(1);

            return Ok(await _service.ListarVentasAsync(DateTime.Parse(fechaInicio), 
                fin, page,
                filas));
        }
        catch (FormatException e)
        {
            _logger.LogWarning(e, "{fechaInicio} {fechaFin} {Message}", fechaInicio, fechaFin, e.Message);
            return BadRequest("Formato de fecha incorrecto");
        }
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard(string fechaInicio, string fechaFin)
    {
        try
        {
            return Ok(await _service.GetDashboardAsync(DateTime.Parse(fechaInicio), 
                DateTime.Parse(fechaFin)));
        }
        catch (FormatException e)
        {
             _logger.LogWarning(e, "{fechaInicio} {fechaFin} {Message}", fechaInicio, fechaFin, e.Message);
            return BadRequest("Formato de fecha incorrecto");
        }
    }
}