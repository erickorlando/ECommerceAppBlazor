using AutoMapper;
using ECommerceApp.Blazor.Shared.Request;
using ECommerceApp.Blazor.Shared.Response;
using ECommerceApp.Entities;
using ECommerceApp.Entities.Infos;

namespace ECommerceApp.Services.Profiles;

public class ProductoProfile : Profile
{
    public ProductoProfile()
    {
        // Es para las listas
        CreateMap<ProductoInfo, ProductoDto>();

        // Para el GET Individual
        CreateMap<Producto, ProductoDto>();

        // Para el POST y PUT
        CreateMap<ProductoDtoRequest, Producto>();
    }
}