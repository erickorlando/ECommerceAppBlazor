using ECommerceApp.Blazor.Shared.Request;
using ECommerceApp.Blazor.Shared.Response;
using ECommerceApp.DataAccess;
using ECommerceApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Blazor.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly ECommerceDbContext _context;

    public ProductosController(ECommerceDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        // Eager Loading

        //var productos = await _context.Set<Producto>()
        //    .Include(x => x.Categoria)
        //    .Where(p => p.Estado)
        //    .AsNoTracking()
        //    .ToListAsync();

        //var lista = productos.Select(p => new ProductoDto
        //{
        //    Id = p.Id,
        //    Categoria = p.Categoria.NombreCategoria,
        //    Nombre = p.Nombre,
        //    PrecioUnitario = p.PrecioUnitario,
        //}).ToList();

        // Lazy Loading

        var productos = await _context.Set<Producto>()
            .Where(p => p.Estado)
            .Select(p => new ProductoDto
            {
                Id = p.Id,
                Categoria = p.Categoria.NombreCategoria,
                Nombre = p.Nombre,
                PrecioUnitario = p.PrecioUnitario,
            })
            .AsNoTracking()
            .ToListAsync();

        return Ok(productos);
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductoDtoRequest request)
    {
        var producto = new Producto()
        {
            Nombre = request.Nombre,
            CodigoSku = request.CodigoSku,
            CategoriaId = request.IdCategoria,
            PrecioUnitario = request.PrecioUnitario,
        };

        await _context.Set<Producto>().AddAsync(producto);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var data = await _context.Set<Producto>()
            .FindAsync(id);

        if (data == null)
            return NotFound();

        var producto = new ProductoDto
        {
            Id = data.Id,
            Nombre = data.Nombre,
            PrecioUnitario = data.PrecioUnitario,
            IdCategoria = data.CategoriaId,
        };

        return Ok(producto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, ProductoDtoRequest request)
    {
        var data = await _context.Set<Producto>()
                    .FindAsync(id);

        if (data == null)
            return NotFound();

        data.Nombre = request.Nombre;
        data.CodigoSku = request.CodigoSku;
        data.CategoriaId = request.IdCategoria;
        data.PrecioUnitario = request.PrecioUnitario;

        await _context.SaveChangesAsync();

        return Ok();
    }
}