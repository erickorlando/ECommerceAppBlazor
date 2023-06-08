using ECommerceApp.Blazor.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceApp.DataAccess;

public static class UserDataSeeder
{
    public static async Task Seed(IServiceProvider service)
    {
        // UserManager (Repositorio de Usuarios)
        var userManager = service.GetRequiredService<UserManager<IdentityUserECommerce>>();
        // RoleManager (Repositorio de Roles)
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

        // Crear Roles
        var adminRole = new IdentityRole(Constantes.RolAdministrador);
        var userRole = new IdentityRole(Constantes.RolUsuario);

        await roleManager.CreateAsync(adminRole);
        await roleManager.CreateAsync(userRole);

        var adminUser = new IdentityUserECommerce
        {
            Nombres = "Administrador",
            Apellidos = "del Sistema",
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com",
            PhoneNumber = "+51 999 999 999",
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminUser, "Admin1234*");
        if (result.Succeeded)
        {
            // Esto me asegura que el usuario se creó correctamente
            adminUser = await userManager.FindByEmailAsync(adminUser.Email);
            if (adminUser is not null)
                await userManager.AddToRoleAsync(adminUser, Constantes.RolAdministrador);
        }
    }
}