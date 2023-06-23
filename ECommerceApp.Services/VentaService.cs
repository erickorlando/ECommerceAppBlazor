using AutoMapper;
using ECommerceApp.Blazor.Shared.Request;
using ECommerceApp.Blazor.Shared.Response;
using ECommerceApp.Entities;
using ECommerceApp.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace ECommerceApp.Services;

public class VentaService : IVentaService
{
    private readonly IVentaRepository _repository;
    private readonly ILogger<VentaService> _logger;
    private readonly IClienteRepository _clienteRepository;
    private readonly IProductoRepository _productoRepository;
    private readonly IMapper _mapper;

    public VentaService(IVentaRepository repository, 
        ILogger<VentaService> logger, 
        IClienteRepository clienteRepository, 
        IProductoRepository productoRepository,
        IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _clienteRepository = clienteRepository;
        _productoRepository = productoRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse> AddAsync(string email, SaleDtoRequest request)
    {
        var response = new BaseResponse();

        try
        {
            // Creamos la transaccion
            await _repository.CrearTransaccionAsync();
            var cliente = await _clienteRepository.FindByEmailAsync(email);
            if (cliente is null)
                throw new InvalidOperationException("El cliente no existe");

            var venta = new Venta
            {
                IdCliente = cliente.Id,
            };

            await _repository.AddAsync(venta);

            // Buscamos los productos.
            var listaPrecios = await _productoRepository.GetPreciosAsync(request.Details.Select(p => p.ProductoId));
            var listaDetalles = new List<VentaDetalle>();

            int contador = 0;
            foreach (var detail in request.Details)
            {
                contador++;
                var ventaDetalle = new VentaDetalle
                {
                    IdVentaNavigation = venta,
                    IdProducto = detail.ProductoId,
                    Cantidad = detail.Cantidad,
                    PrecioUnitario = listaPrecios[detail.ProductoId],
                    Correlativo = contador
                };

                var montoSinImpuesto = ventaDetalle.PrecioUnitario / 1.18m;

                ventaDetalle.Total = montoSinImpuesto * ventaDetalle.Cantidad;

                // Agregamos los detalles al contexto de la venta
                await _repository.AddVentaDetalleAsync(ventaDetalle);
                listaDetalles.Add(ventaDetalle);
            }

            // Calculamos los impuestos.
            venta.SubTotal = listaDetalles.Sum(p => p.Total);
            venta.Impuestos = venta.SubTotal * 0.18m;
            venta.Total = venta.SubTotal + venta.Impuestos;

            // Este Update guardara en la base de datos y hara el commit
            await _repository.UpdateAsync();
            response.Success = true;
        }
        catch (Exception ex)
        {
            await _repository.RollBackAsync();
            response.ErrorMessage = "Error al crear la venta";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<PaginationResponse<VentaDtoResponse>> ListarVentasAsync(DateTime fechaInicio, DateTime fechaFin, int page, int cantidad)
    {
        var response = new PaginationResponse<VentaDtoResponse>();

        try
        {
            var tupla = await _repository.ListarVentas(fechaInicio, fechaFin, page, cantidad);
            var totalPages = tupla.Total / cantidad;
            if (tupla.Total % cantidad != 0)
                totalPages++;
            response.Data = _mapper.Map<ICollection<VentaDtoResponse>>(tupla.Collection);
            response.TotalPages = totalPages;
            response.Success = true;
        }
        catch (Exception ex)
        {
             response.ErrorMessage = "Error al listar las ventas";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<DashboardDtoResponse>> GetDashboardAsync(DateTime fechaInicio, DateTime fechaFin)
    {
        var response = new BaseResponseGeneric<DashboardDtoResponse>();

        try
        {
            var dashboard = await _repository.GetDashboardInfoAsync(fechaInicio, fechaFin);
            response.Data = _mapper.Map<DashboardDtoResponse>(dashboard);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al obtener el dashboard";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }
}