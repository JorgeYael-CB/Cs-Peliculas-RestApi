using PeliculasAPI;
using static PeliculasAPI.EjemploTiempoDeVidaServicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// cache
builder.Services.AddOutputCache(opciones =>
{
    opciones.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(60);
});

// Servicios - DI
builder.Services.AddSingleton<IRepository, RepositorySqlServer>(); // singleton

// Tiempo de vida diferentes - DI
builder.Services.AddTransient<ServicioTransient>();
builder.Services.AddScoped<ServicioScope>();
builder.Services.AddSingleton<ServicioSingleton>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// use cache
app.UseOutputCache();

app.UseAuthorization();

app.MapControllers();

app.Run();
