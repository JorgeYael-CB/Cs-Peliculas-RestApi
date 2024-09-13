using Microsoft.EntityFrameworkCore;

namespace PeliculasAPI.Utilidades;




public static class HttpContextExtensions
{
    // Metodos de extension
    public async static Task InsertarParametrosPaginacionEnCabecera<T>(this HttpContext context,
        IQueryable<T> queryable) // querys para base de datos
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        double cantidad = await queryable.CountAsync();

        // lo asignamos en el header de la respuesta HTTP
        context.Response.Headers.Append("cantidad-total-registros", cantidad.ToString());
    }
}
