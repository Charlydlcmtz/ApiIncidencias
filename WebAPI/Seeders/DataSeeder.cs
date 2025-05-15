using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WebAPI.Seeders
{
    public static class DataSeeder
    {
        public static void SeedUsuarioDummy(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<Infraestructure.Data.ApplicationDbContext>();

            if (!context.Usuarios.Any(u => u.Username == "Charlydlcmtz"))
            {
                var user = new Usuario
                {
                    Username = "Charlydlcmtz",
                    Nombre = "Carlos",
                    ApellidoP = "De La Cruz",
                    ApellidoM = "Martínez",
                    Correo = "cdelacruz@tc.com.mx",
                    Rol = "Admin",
                    Activo = true
                };

                var hasher = new PasswordHasher<Usuario>();
                user.ContrasenaHash = hasher.HashPassword(user, "Jacobyshidix1.");

                context.Usuarios.Add(user);
                context.SaveChanges();
            }
        }
    }
}
