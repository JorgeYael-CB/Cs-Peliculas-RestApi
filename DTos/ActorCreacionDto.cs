using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;



namespace PeliculasAPI.DTos;

public class ActorCreacionDto
{
    [Required]
    [StringLength(150)]
    public string Nombre { get; set; } = null!;
    public DateTime FechaNacimiento { get; set; }
    [Unicode(false)]
    public IFormFile? Foto { get; set; }
}
