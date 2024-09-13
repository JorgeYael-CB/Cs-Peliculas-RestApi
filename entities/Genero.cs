using System;
using System.ComponentModel.DataAnnotations;
using PeliculasAPI.Validaciones;

namespace PeliculasAPI.entities;

public class Genero: IId
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido")] // validacion de ASP.NET Core
    [StringLength(50, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos")]
    [PrimeraLetraMayusculaAtributo]
    public required string Nombre { get; set; }


    // [Range(18, 50)]
    // public int? Edad { get; set; }
    // [CreditCard(ErrorMessage = "Eso no es una tarjeta de credito valida")]
    // public string? TarjetaCredito { get; set; }
    // [Url]
    // public string? Url { get; set; }
}
