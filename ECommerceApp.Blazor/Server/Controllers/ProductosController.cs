using ECommerceApp.Blazor.Shared.Request;
using ECommerceApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Blazor.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IProductoService _service;

    public ProductosController(IProductoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string? filter, int page = 1, int rows = 5)
    {
        var response = await _service.ListAsync(filter, page, rows);

        return Ok(response);
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> List(string? filtro)
    {
        var response = await _service.ListAsync(filtro);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductoDtoRequest request)
    {
        return Ok(await _service.AddAsync(request));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)    
    {
        var response = await _service.FindByIdAsync(id);

        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, ProductoDtoRequest request)
    {
        return Ok(await _service.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _service.DeleteAsync(id));
    }
}