using Application.Interfaces;
using Application.Services;
using Domain.Repositories;
using Infraestructure.Data;
using Infrastructure.Identity;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using FluentValidation;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"));
});

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IIncidenciaRepository, IncidenciaRepository>();
builder.Services.AddScoped<IIncidenciaService, IncidenciaService>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ITipoIncidenciaRepository, TipoIncidenciaRepository>();
builder.Services.AddScoped<ITipoIncidenciaService, TipoIncidenciaService>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddValidatorsFromAssemblyContaining<CrearUsuarioDtoValidator>(); // Agrega validadores de FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CrearCategoriaIncidenciaDto>(); // Agrega validadores de FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<ActualizarCategoriaIncidenciaDtoValidator>(); // Agrega validadores de FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CrearIncidenciaDtoValidator>(); // Agrega validadores de FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<ActualizarIncidenciaDtoValidator>(); // Agrega validadores de FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CrearTipoIncidenciaDtoValidator>(); // Agrega validadores de FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<ActualizarTipoIncidenciaDtoValidator>(); // Agrega validadores de FluentValidation

var claveSecreta = builder.Configuration.GetValue<string>("ApiSettings:Secreta");

//Aqui se configura la autenticacion
builder.Services.AddAuthentication
    ( options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    )
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(claveSecreta)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors
                .Select(error => new { campo = x.Key, mensaje = error.ErrorMessage }))
            .ToList();

        return new BadRequestObjectResult(new
        {
            status = "error",
            message = "Error de validación",
            errors
        });
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Autenticación JWT usando el esquema Bearer, \r\n\r\n " +
        "Ingresa la palabra 'Bearer' seguido de un [espacio] y despues su token en el campo de abajo.\r\n\r\n" +
        "Ejemplo: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.0",
        Title = "API de Prueba",
        Description = "API de Incidencias arquitectura limpia.",
        TermsOfService = new Uri("https://github.com/Charlydlcmtz?tab=repositories"),
        Contact = new OpenApiContact
        {
            Name = "Charlydlcmtz",
            Url = new Uri("https://github.com/Charlydlcmtz?tab=repositories")
        },
        License = new OpenApiLicense
        {
            Name = "Licencia de uso Personal",
            Url = new Uri("https://github.com/Charlydlcmtz?tab=repositories")
        }
    });
});

//Soporte para CORS
//Se pueden Habilitar: 1-Un dominio especifico, 2-multiples dominios, 3-cualquier dominio (Tener en cuenta seguridad)
//Usamos de ejemplo el dominio: http://localhost:4200, se debe cambiar por el correcto
//Se usa (*) para todos los dominios
builder.Services.AddCors(c => c.AddPolicy("PoliticaCors", build =>
{
    build.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// ⬇️ Se ejecuta el seeder aquí ⬇️
SeedUsuarioDummy(app.Services);

//Para usar Swagger en producción
app.UseSwagger();
app.UseSwaggerUI();

//Soporte para archivos estaticos como imagenes
app.UseStaticFiles();

//Soporte para CORS
app.UseCors("PoliticaCors");

//Soporte para authenticacion
app.UseHttpsRedirection();
app.UseMiddleware<WebAPI.Middleware.ErrorHandlingMiddleware>(); // Middleware para JWT
app.UseAuthentication(); // Esta debe ir antes de UseAuthorization()
app.UseAuthorization();

app.MapControllers();

app.Run();

// ⬇️ Método Seeder (puede moverse a clase aparte luego) ⬇️
static void SeedUsuarioDummy(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    context.Database.Migrate(); // aplica migraciones si no se han aplicado

    if (!context.Usuarios.Any(u => u.Username == "Charlydlcmtz"))
    {
        var user = new Domain.Entities.Usuario
        {
            Username = "Charlydlcmtz",
            Nombre = "Carlos",
            ApellidoP = "De La Cruz",
            ApellidoM = "Martínez",
            Correo = "cdelacruz@tc.com.mx",
            Rol = "Admin",
            Activo = true
        };

        var hasher = new PasswordHasher<Domain.Entities.Usuario>();
        user.ContrasenaHash = hasher.HashPassword(user, "Jacobyshidix1.");

        context.Usuarios.Add(user);
        context.SaveChanges();
    }
}
