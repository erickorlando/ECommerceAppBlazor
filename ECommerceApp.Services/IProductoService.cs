using ECommerceApp.Blazor.Shared.Request;
using ECommerceApp.Blazor.Shared.Response;

namespace ECommerceApp.Services
{
    public interface IProductoService
    {
        Task<PaginationResponse<ProductoDto>> ListAsync(string? filter, int page, int rows);

        Task<BaseResponseGeneric<ICollection<ProductoDto>>> ListAsync(string? filter);

        Task<BaseResponseGeneric<ProductoDto>> FindByIdAsync(int id);

        Task<BaseResponseGeneric<int>> AddAsync(ProductoDtoRequest request);

        Task<BaseResponse> UpdateAsync(int id, ProductoDtoRequest request);

        Task<BaseResponse> DeleteAsync(int id);
    }
}