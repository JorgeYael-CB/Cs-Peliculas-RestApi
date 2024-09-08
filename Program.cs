

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
            .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// use cors
app.UseCors();

app.UseHttpsRedirection();

// use cache
app.UseOutputCache();

app.UseAuthorization();

app.MapControllers();

app.Run();
