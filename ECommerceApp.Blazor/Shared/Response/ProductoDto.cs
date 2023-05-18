﻿namespace ECommerceApp.Blazor.Shared.Response;

public class ProductoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Categoria { get; set; } = null!;
    public decimal PrecioUnitario { get; set; }
}