using ECommerceApp.Blazor.Shared.Response;
using ECommerceApp.DataAccess;
using ECommerceApp.Repositories.Implementations;
using ECommerceApp.Repositories.Interfaces;
using ECommerceApp.Services;
using ECommerceApp.Services.Profiles;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ECommerceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceDb"));
    options.EnableSensitiveDataLogging();
});

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<ProductoProfile>();
});

builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<IProductoRepository, ProductoRepository>();

builder.Services.AddTransient<IProductoService, ProductoService>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.MapGet("api/Categorias", async (ICategoriaRepository repository) =>
{
    var collection = await repository.ListAsync();

    return Results.Ok(collection);
});

app.Run();
