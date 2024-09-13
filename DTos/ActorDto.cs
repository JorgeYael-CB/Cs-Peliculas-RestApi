namespace PeliculasAPI.DTos;



public class ActorDto
{
    public int Id { get; set; }
    public required string Nombre { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string? Foto { get; set; }
}
