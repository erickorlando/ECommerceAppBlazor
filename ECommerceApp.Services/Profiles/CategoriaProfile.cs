using AutoMapper;
using ECommerceApp.Blazor.Shared.Response;
using ECommerceApp.Entities;

namespace ECommerceApp.Services.Profiles;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<Categoria, CategoriaDto>()
            .ForMember(dest => dest.Id, orig => orig.MapFrom(x => x.Id))
            .ForMember(dest => dest.Nombre, orig => orig.MapFrom(x => x.NombreCategoria))
            .ReverseMap();
    }
}