namespace PeliculasAPI.services;




public interface IAlmacenadorArchivos
{
    Task<string> Almacenar( string contenedor, IFormFile file );
    Task Borrar(string? ruta, string contenedor );
    async Task<string> Editar(string? ruta, string contenedor, IFormFile file)
    {
        await this.Borrar( ruta, contenedor );
        string newFile = await this.Almacenar( contenedor, file );

        return newFile;
    }
}
