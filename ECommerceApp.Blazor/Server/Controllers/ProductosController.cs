using System.Linq.Expressions;
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
    public async Task<IActionResult> Get(string? filter, int page = 1, int rows = 5)
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

        var response = new PaginationResponse<ProductoDto>();

        Expression<Func<Producto, bool>> predicate = p => p.Estado 
                                                           && p.Nombre.Contains(filter ?? string.Empty);
        var productos = await _context.Producto
            .Where(predicate)
            .OrderBy(p => p.CodigoSku)
            .Skip((page - 1) * rows)
            .Take(rows)
            .Select(p => new ProductoDto
            {
                Id = p.Id,
                Categoria = p.Categoria.NombreCategoria,
                Nombre = p.Nombre,
                PrecioUnitario = p.PrecioUnitario,
                Comentarios = p.Comentarios
            })
            .AsNoTracking()
            .ToListAsync();

        var count = await _context.Producto
            .Where(predicate)
            .CountAsync();

        response.Data = productos;
        var totalPages = count / rows;
        if (count % rows != 0)
            totalPages++;

        response.TotalPages = totalPages;
        response.Success = true;

        return Ok(response);
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