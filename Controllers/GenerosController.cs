using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using PeliculasAPI.entities;



namespace PeliculasAPI.Controllers
{
    [Route("api/generos")] // ruta base - http
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly IOutputCacheStore cacheStore;
        private const string cacheTag = "generos";


        // DI
        public GenerosController ( IOutputCacheStore cacheStore )
        {
            this.cacheStore = cacheStore;
        }

        [HttpGet]
        [OutputCache(Tags = [cacheTag])]
        public List<Genero> Get(){
            return new List<Genero> () {
                new Genero {Nombre= "Comedia", Id=1},
                new Genero {Nombre= "Accion", Id=2}
            };
        }


        [HttpGet("{id:int}")]
        [OutputCache(Tags = [cacheTag])] // usar cache
        public async Task<ActionResult<Genero>> Get(int id)
        {
            throw new NotImplementedException();
        }


        [HttpPost]
        public async Task<ActionResult<Genero>> Post( [FromBody] Genero genero )
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public void Delete(){
            throw new NotImplementedException();
        }

        [HttpPut]
        public void Put(){
            throw new NotImplementedException();
        }
    }
}
