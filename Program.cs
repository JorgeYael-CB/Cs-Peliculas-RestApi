

using Microsoft.EntityFrameworkCore;
using PeliculasAPI;
using PeliculasAPI.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Automapper
builder.Services.AddAutoMapper(typeof(Program));

// DB Conexion
builder.Services.AddDbContext<ApplicationDbContext>( opciones =>
    opciones.UseSqlServer("name=DefaultConnection") );

// cache
builder.Services.AddOutputCache(opciones =>
{
    opciones.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(60);
});

// Obtener URLs permitidas
var origenesPermitidos = builder.Configuration.GetValue<string>("origenetesPermitidos")!
    .Split(",");

// Configurando CORS
builder.Services.AddCors( opciones =>
{
    opciones.AddDefaultPolicy( opcionesCORS =>
    {
        opcionesCORS.WithOrigins(origenesPermitidos)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("cantidad-total-registros");
    });
});

// DI
builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// use cors
app.UseCors();

app.UseHttpsRedirection();

//Middlewares
app.UseStaticFiles();

// use cache
app.UseOutputCache();

app.UseAuthorization();

app.MapControllers();

app.Run();
