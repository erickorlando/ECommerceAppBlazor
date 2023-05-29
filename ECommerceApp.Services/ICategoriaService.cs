using ECommerceApp.Blazor.Shared.Response;

namespace ECommerceApp.Services;

public interface ICategoriaService
{
    Task<BaseResponseGeneric<ICollection<CategoriaDto>>> ListAsync();

    Task<BaseResponseGeneric<CategoriaDto>> FindByIdAsync(int id);

    Task<BaseResponseGeneric<int>> AddAsync(CategoriaDto request);

    Task<BaseResponse> UpdateAsync(int id, CategoriaDto request);

    Task<BaseResponse> DeleteAsync(int id);
}