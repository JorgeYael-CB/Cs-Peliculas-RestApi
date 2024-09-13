using PeliculasAPI.entities;

namespace PeliculasAPI.DTos;



public class GeneroDto: IId
{
    public int Id { get; set; }
    public required string Nombre { get; set; }
}
