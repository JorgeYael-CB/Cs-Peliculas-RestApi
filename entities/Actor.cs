using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PeliculasAPI.entities;



public class Actor: IId
{
    public int Id { get; set; }
    [Required]
    [StringLength(150)]
    public string Nombre { get; set; } = null!;
    public DateTime FechaNacimiento { get; set; }
    [Unicode(false)]
    public string? Foto { get; set; }
}
