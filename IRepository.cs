using PeliculasAPI.entities;


namespace PeliculasAPI;

public interface IRepository
{
    public List<Genero> GetGeneros();
    public Task<Genero?> GetById(int id);
    public bool Existe(string nombre);
    public void Crear (Genero genero);
}
