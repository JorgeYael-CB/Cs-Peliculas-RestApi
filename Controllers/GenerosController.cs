using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using PeliculasAPI.entities;
using static PeliculasAPI.EjemploTiempoDeVidaServicios;



namespace PeliculasAPI.Controllers
{
    [Route("api/generos")] // ruta base - http
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly ServicioTransient transient2;
        private readonly ServicioScope scope1;
        private readonly ServicioScope scope2;
        private readonly ServicioSingleton singleton1;
        private readonly IOutputCacheStore cacheStore;
        private readonly IConfiguration configuration;

        private IRepository Repo { get; set; }
        public ServicioTransient Transient1 { get; }
        private const string cacheTag = "generos";


        // DI
        public GenerosController (
            IRepository Repo,
            ServicioTransient Transient1,
            ServicioTransient Transient2,
            ServicioScope Scope1,
            ServicioScope Scope2,
            ServicioSingleton Singleton1, IOutputCacheStore cacheStore,
            IConfiguration configuration)
        {
            this.Repo = Repo;
            this.Transient1 = Transient1;
            transient2 = Transient2;
            scope1 = Scope1;
            scope2 = Scope2;
            singleton1 = Singleton1;
            this.cacheStore = cacheStore;
            this.configuration = configuration;
        }

        [HttpGet("servicios-tiempos")]
        public IActionResult GetServicioTiempoVida()
        {
            return Ok(
                new {
                    Transients = new {
                        transient1 = Transient1.GetId(),
                        transient2 = transient2.GetId()
                    },
                    scopeds = new {
                        scope1 = scope1.GetId(),
                        scope2 = scope2.GetId()
                    },
                    singleton = this.singleton1.GetId(),
                }
            );
        }

        [HttpGet("ejemplo-provedor-config")]
        public string GetEjemploProvedorConfig()
        {
            return this.configuration.GetValue<string>("CadenaDeConexion")!; // desarrollo
        }



        [HttpGet]
        [HttpGet("listado")] // dos rutas, mismo valor.
        [HttpGet("/listado-generos")]
        [OutputCache(Tags = [cacheTag])]
        public List<Genero> Get(){
            return Repo.GetGeneros();
        }

        [HttpGet("{id:int}")]
        [OutputCache(Tags = [cacheTag])] // usar cache
        public async Task<ActionResult<Genero>> Get(int id)
        {
            Genero? genero = await Repo.GetById(id);

            if( genero is null )
            {
                return NotFound();
            }

            return genero;
        }

        //? retornar void en un task
        // private async Task LoguearEnConsola()
        // {
        //     Console.WriteLine("Saludos");
        // }

        [HttpGet("{nombre}")] // parametros en la URL
        public string Get(string nombre)
        {
            return nombre;
        }


        [HttpPost]
        public async Task<ActionResult<Genero>> Post( [FromBody] Genero genero )
        {
            var yaExisteGeneroNombre = Repo.Existe(genero.Nombre);

            if( yaExisteGeneroNombre )
            {
                return BadRequest($"Ya existe un genero con el nombre {genero.Nombre}");
            }

            Repo.Crear(genero);
            // Limpiar cache
            await this.cacheStore.EvictByTagAsync(cacheTag, default); // tag referencial del cache

            return Ok(genero);
        }

        [HttpDelete]
        public void Delete(){}

        [HttpPut]
        public void Put(){}
    }
}
