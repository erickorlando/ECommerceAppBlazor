using AutoMapper;
using ECommerceApp.Blazor.Shared.Request;
using ECommerceApp.Blazor.Shared.Response;
using ECommerceApp.Entities;
using ECommerceApp.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace ECommerceApp.Services;

public class ProductoService : IProductoService
{
    private readonly IProductoRepository _repository;
    private readonly ILogger<ProductoService> _logger;
    private readonly IMapper _mapper;

    public ProductoService(IProductoRepository repository, ILogger<ProductoService> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<ProductoDto>> ListAsync(string? filter, int page, int rows)
    {
        var response = new PaginationResponse<ProductoDto>();

        try
        {
            var tupla = await _repository.ListAsync(filter, page, rows);
            response.Data = _mapper.Map<ICollection<ProductoDto>>(tupla.Collection);

            var totalPages = tupla.Total / rows;
            if (tupla.Total % rows != 0)
                totalPages++;

            response.TotalPages = totalPages;
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Listar los productos";
            _logger.LogCritical(ex, "{ErrorMessage} {Message} ", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<ICollection<ProductoDto>>> ListAsync(string? filter)
    {
        var response = new BaseResponseGeneric<ICollection<ProductoDto>>();

        try
        {
            response.Data = await _repository.ListAsync(p => p.Estado && p.Nombre.Contains(filter ?? string.Empty),
                p => _mapper.Map<ProductoDto>(p));

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Listar los productos";
            _logger.LogCritical(ex, "{ErrorMessage} {Message} ", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<ProductoDto>> FindByIdAsync(int id)
    {
        var response = new BaseResponseGeneric<ProductoDto>();

        try
        {
            var producto = await _repository.FindByIdAsync(id);
            if (producto == null)
            {
                throw new ApplicationException("No se encontro el registro");
            }

            response.Data = _mapper.Map<ProductoDto>(producto);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al seleccionar el producto";
            _logger.LogCritical(ex, "{ErrorMessage} {Message} ", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<int>> AddAsync(ProductoDtoRequest request)
    {
        var response = new BaseResponseGeneric<int>();

        try
        {
            var producto = _mapper.Map<Producto>(request);

            response.Data = await _repository.AddAsync(producto);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Grabar el producto";
            _logger.LogCritical(ex, "{ErrorMessage} {Message} ", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponse> UpdateAsync(int id, ProductoDtoRequest request)
    {
        var response = new BaseResponse();

        try
        {
            var producto = await _repository.FindByIdAsync(id);
            if (producto == null)
            {
                throw new ApplicationException("No se encontro el registro");
            }

            // Aqui se hace el mapeo a una instancia ya existente
            // y se reemplazan sus valores
            _mapper.Map(request, producto);

            await _repository.UpdateAsync();
            response.Success = true;

        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Actualizar el producto";
            _logger.LogCritical(ex, "{ErrorMessage} {Message} ", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponse> DeleteAsync(int id)
    {
        var response = new BaseResponse();

        try
        {
            await _repository.DeleteAsync(id);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Eliminar el producto";
            _logger.LogCritical(ex, "{ErrorMessage} {Message} ", response.ErrorMessage, ex.Message);
        }

        return response;

    }
}