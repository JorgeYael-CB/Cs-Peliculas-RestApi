using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PeliculasAPI.DTos;
using PeliculasAPI.entities;
using PeliculasAPI.Utilidades;



namespace PeliculasAPI.Controllers
{
    [Route("api/generos")] // ruta base - http
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly IOutputCacheStore cacheStore;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private const string cacheTag = "generos";


        // DI
        public GenerosController ( IOutputCacheStore cacheStore,
            ApplicationDbContext context, IMapper mapper)
        {
            this.cacheStore = cacheStore;
            this.context = context;
            this.mapper = mapper;
        }


        //* Obtener todos los generos
        [HttpGet]
        [OutputCache(Tags = [cacheTag])]
        public async Task<List<GeneroDto>> Get([FromQuery] PaginacionDTO paginacion){
            var queryable = context.Generos;
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);

            return await queryable
                .OrderBy( g => g.Id ) // buenas practicas
                .Paginar(paginacion) // clase - IQueryableExtensions
                .ProjectTo<GeneroDto>(mapper.ConfigurationProvider)
                .ToListAsync();
        }


        //* Obtener genero por Id
        [HttpGet("{id:int}", Name = "ObtenerGeneroPorId")]
        [OutputCache(Tags = [cacheTag])] // usar cache
        public async Task<ActionResult<GeneroDto>> Get(int id)
        {
            var genero = await context.Generos
                .ProjectTo<GeneroDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(g => g.Id == id);

            if( genero is null ) return NotFound();
            return genero;
        }


        //* Crear genero
        [HttpPost]
        public async Task<ActionResult<Genero>> Post([FromBody] GeneroCreacionDto generoCreacionDto )
        {
            var genero = mapper.Map<Genero>( generoCreacionDto);

            context.Add(genero); // preparar cambios
            await context.SaveChangesAsync(); // guardar cambios
            await cacheStore.EvictByTagAsync(cacheTag, default); // limpiar cache cuando se crea un genero

            // Retornamos url para obtener el genero
            return CreatedAtRoute("ObtenerGeneroPorId", new {id = genero.Id},genero);
        }


        //* Eliminar un genero
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var registroBorrados = await context.Generos
                .Where(g => g.Id == id)
                .ExecuteDeleteAsync();

            if( registroBorrados == 0 )
            {
                return NotFound();
            }
            await cacheStore.EvictByTagAsync(cacheTag, default);
            return NoContent();
        }


        //* Actualizar un genero
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put( int id, [FromBody] GeneroCreacionDto genero ){
            var generoExiste = await context.Generos.AnyAsync( g => g.Id == id);

            if( !generoExiste )
            {
                return NotFound();
            }

            var gen = mapper.Map<Genero>(genero);
            gen.Id = id;

            context.Update(gen);
            await context.SaveChangesAsync(); // guardamos cambios
            await cacheStore.EvictByTagAsync(cacheTag, default); // limpiar cache cuando se crea un genero

            return NoContent();
        }
    }
}
