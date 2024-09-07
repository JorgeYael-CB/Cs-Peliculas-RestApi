using PeliculasAPI.entities;

namespace PeliculasAPI;


public class RepositorioEnMemoria: IRepository // implementa de esa interface
{
    private List<Genero> _generos;

    public RepositorioEnMemoria(){
        _generos = new List<Genero>()
        {
            new Genero{Id=1, Nombre="Comedia"},
            new Genero{Id=2, Nombre="Accion"},
        };
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

    public void Crear(Genero genero)
    {
        throw new NotImplementedException();
    }
}
