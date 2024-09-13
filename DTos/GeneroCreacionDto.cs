using System.ComponentModel.DataAnnotations;
using PeliculasAPI.Validaciones;



namespace PeliculasAPI.DTos;

public class GeneroCreacionDto
{
    [Required(ErrorMessage = "El campo {0} es requerido")] // validacion de ASP.NET Core
    [StringLength(50, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos")]
    [PrimeraLetraMayusculaAtributo]
    public required string Nombre { get; set; }
}
