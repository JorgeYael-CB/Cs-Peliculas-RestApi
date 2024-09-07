using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Validaciones;



public class PrimeraLetraMayusculaAtributo: ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if( value is null || string.IsNullOrWhiteSpace(value.ToString()) )
        {
            return ValidationResult.Success;
        }

        string primeraLetra = value.ToString()![0].ToString();
        if( primeraLetra != primeraLetra.ToUpper())
        {
            return new ValidationResult("La primera letra debe ser mayuscula");
        }

        return ValidationResult.Success;
    }
}
