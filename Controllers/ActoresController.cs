using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTos;
using PeliculasAPI.entities;
using PeliculasAPI.services;
using PeliculasAPI.Utilidades;




namespace PeliculasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActoresController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IOutputCacheStore cache;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private const string cacheTag = "actores";
        private readonly string contenedor = "actores";


        public ActoresController(ApplicationDbContext context, IMapper mapper,
        IOutputCacheStore cache, IAlmacenadorArchivos almacenadorArchivos): base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.cache = cache;
            this.almacenadorArchivos = almacenadorArchivos;
        }



        [HttpGet]
        [OutputCache(Tags = [cacheTag])]
        public async Task<List<ActorDto>> Get( [FromQuery] PaginacionDTO paginacionDTO )
        {
            return await Get<Actor, ActorDto>( paginacionDTO, ordenarPor: g => g.Id );
        }


        [HttpGet("{id:int}", Name ="ObtenerActorPorId")]
        [OutputCache(Tags = [cacheTag,])]
        public async Task<ActionResult<ActorDto>> GetById(int id)
        {
            var actor = await context.Actores.ProjectTo<ActorDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == id);

            if( actor is null )
            {
                return NotFound();
            }
            return actor;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ActorCreacionDto actorCreacionDto)
        {
            var actor = mapper.Map<Actor>(actorCreacionDto);
            Console.WriteLine("Ya entramos...");

            if(actorCreacionDto.Foto is not null)
            {
                string url = await almacenadorArchivos.Almacenar(contenedor, actorCreacionDto.Foto);
                actor.Foto = url;
            }

            context.Add(actor);
            await context.SaveChangesAsync();
            await cache.EvictByTagAsync(cacheTag, default);

            return CreatedAtRoute("ObtenerActorPorId", new {id = actor.Id}, actor);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put( int id, [FromForm] ActorCreacionDto actorCreacionDto)
        {
            var actor = await context.Actores.FirstOrDefaultAsync(a => a.Id == id);
            if( actor == null ) return NotFound();

            actor = mapper.Map(actorCreacionDto, actor);

            if( actorCreacionDto.Foto is not null )
            {
                actor.Foto = await almacenadorArchivos
                    .Editar(actor.Foto, contenedor, actorCreacionDto.Foto);
            }

            await context.SaveChangesAsync();
            await cache.EvictByTagAsync(cacheTag, default);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var registrosBorrados = await this.context.Actores
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync();

            if( registrosBorrados == 0 )
            {
                NotFound();
            }

            await cache.EvictByTagAsync(cacheTag, default);

            return NoContent();
        }
    }
}
