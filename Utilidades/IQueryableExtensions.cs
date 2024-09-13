using PeliculasAPI.DTos;

namespace PeliculasAPI.Utilidades;



public static class IQueryableExtensions
{
    public static IQueryable<T> Paginar<T>(this IQueryable<T> query, PaginacionDTO paginacion)
    {
        return query
            .Skip( (paginacion.Pagina - 1) * paginacion.RecordsPorPagina )
            .Take(paginacion.RecordsPorPagina);
    }
}
