using Microsoft.EntityFrameworkCore;
using PeliculasAPI.entities;

namespace PeliculasAPI;



public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions options): base(options)
    {
    }

    public DbSet<Genero> Generos { get; set; } // columna para la base de datos
    public DbSet<Actor> Actores { get; set; }
}
