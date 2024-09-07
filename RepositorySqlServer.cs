using System;
using PeliculasAPI.entities;

namespace PeliculasAPI;



public class RepositorySqlServer : IRepository
{
    private List<Genero> _generos;

    public RepositorySqlServer(){
        _generos = new List<Genero>()
        {
            new Genero{Id=1, Nombre="Comedia"},
            new Genero{Id=2, Nombre="Accion"},
        };
    }

    public void Crear( Genero genero )
    {
        this._generos.Add( genero );
    }

    public async Task<Genero?> GetById(int id)
    {
        await Task.Delay( TimeSpan.FromSeconds(2) ); // simulando espera y await
        return _generos.FirstOrDefault(g => g.Id==id);
    }

    public List<Genero> GetGeneros()
    {
        return _generos;
    }

    public bool Existe(string nombre)
    {
        return _generos.Any( g => g.Nombre == nombre );
    }
}
