using AutoMapper;
using ECommerceApp.Blazor.Shared.Response;
using ECommerceApp.Entities.Infos;

namespace ECommerceApp.Services.Profiles;

public class VentaProfile : Profile
{
    public VentaProfile()
    {
        CreateMap<VentaInfo, VentaDtoResponse>();
    }
}