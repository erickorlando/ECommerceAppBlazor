using ECommerceApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.DataAccess
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Categoria

            modelBuilder.Entity<Categoria>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Categoria>()
                .Property(p => p.NombreCategoria)
                .HasMaxLength(100);


            // Producto
            modelBuilder.Entity<Producto>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Nombre)
                .HasMaxLength(100);

            modelBuilder.Entity<Producto>()
                .Property(p => p.CodigoSku)
                .HasMaxLength(20);

            modelBuilder.Entity<Producto>()
                .Property(p => p.PrecioUnitario)
                .HasPrecision(11, 2);

            // Data Seeding
            modelBuilder.Entity<Categoria>()
                .HasData(
                    new Categoria { Id = 1, NombreCategoria = "Celulares" },
                    new Categoria { Id = 2, NombreCategoria = "Computadoras" },
                    new Categoria { Id = 3, NombreCategoria = "Televisores" }
                );

            modelBuilder.Entity<Producto>()
                .HasData(
                    new Producto
                        { Id = 1, CategoriaId = 1, Nombre = "Xiaomi Mi3", CodigoSku = "0001", PrecioUnitario = 340 },
                    new Producto
                        { Id = 2, CategoriaId = 2, Nombre = "Laptop ASUS ROG", CodigoSku = "0002", PrecioUnitario = 7499.99m }
                    );
        }
    }
}