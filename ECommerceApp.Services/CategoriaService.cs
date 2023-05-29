using AutoMapper;
using ECommerceApp.Blazor.Shared.Response;
using ECommerceApp.Entities;
using ECommerceApp.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace ECommerceApp.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _repository;
    private readonly ILogger<CategoriaService> _logger;
    private readonly IMapper _mapper;

    public CategoriaService(ICategoriaRepository repository, ILogger<CategoriaService> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<BaseResponseGeneric<ICollection<CategoriaDto>>> ListAsync()
    {
        var response = new BaseResponseGeneric<ICollection<CategoriaDto>>();
        try
        {
            response.Data = _mapper.Map<ICollection<CategoriaDto>>(await _repository.ListAsync());
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al listar las categorias";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponseGeneric<CategoriaDto>> FindByIdAsync(int id)
    {
        var response = new BaseResponseGeneric<CategoriaDto>();
        try
        {
            var categoria = await _repository.FindByIdAsync(id);
            if (categoria == null)
                throw new ApplicationException("No se encontró la categoria");

            response.Data = _mapper.Map<CategoriaDto>(categoria);
            response.Success = true;
        }
        catch (Exception ex)
        {
             response.ErrorMessage = "Error al obtener la categoria";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<int>> AddAsync(CategoriaDto request)
    {
        var response = new BaseResponseGeneric<int>();

        try
        {
            var categoria = _mapper.Map<Categoria>(request);
            categoria.Estado = true;
            await _repository.AddAsync(categoria);
            response.Data = categoria.Id;
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al agregar la categoria";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponse> UpdateAsync(int id, CategoriaDto request)
    {
        var response = new BaseResponse();

        try
        {
            var categoria = await _repository.FindByIdAsync(id);
            if (categoria == null)
                throw new ApplicationException("No se encontró la categoria");

            _mapper.Map(request, categoria);

            await _repository.UpdateAsync();
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al actualizar la categoria";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
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
            response.ErrorMessage = "Error al eliminar la categoria";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }
}