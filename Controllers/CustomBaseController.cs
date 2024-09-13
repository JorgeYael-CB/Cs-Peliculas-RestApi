using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTos;
using PeliculasAPI.Utilidades;

namespace PeliculasAPI.Controllers;




public class CustomBaseController(ApplicationDbContext context, IMapper mapper): ControllerBase
{


    protected async Task<List<TDTO>> Get<TEntidad, TDTO> ( PaginacionDTO paginacionDTO,
        Expression<Func<TEntidad, object>> ordenarPor )
        where TEntidad : class
    {
        var queryable = context.Set<TEntidad>().AsQueryable();
        await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);

        return await queryable
            .OrderBy( ordenarPor )
            .Paginar(paginacionDTO)
            .ProjectTo<TDTO>(mapper.ConfigurationProvider).ToListAsync();
    }

}
