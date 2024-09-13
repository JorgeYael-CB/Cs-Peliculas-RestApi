namespace PeliculasAPI.services;




public class AlmacenadorArchivosLocal : IAlmacenadorArchivos
{
    private readonly IWebHostEnvironment env;
    private readonly IHttpContextAccessor accessor;


    public AlmacenadorArchivosLocal(IWebHostEnvironment env, IHttpContextAccessor accessor)
    {
        this.env = env;
        this.accessor = accessor;
    }


    public async Task<string> Almacenar(string contenedor, IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName);
        var nombreArchivo = $"{Guid.NewGuid()}{extension}";
        string folder = Path.Combine(env.WebRootPath, contenedor);

        if( !Directory.Exists(folder) )
        {
            Directory.CreateDirectory(folder);
        }

        string ruta = Path.Combine(folder, nombreArchivo);

        using( var ms = new MemoryStream() )
        {
            await file.CopyToAsync(ms);
            var contenido = ms.ToArray();
            await File.WriteAllBytesAsync(ruta, contenido);
        }

        var request = accessor.HttpContext!.Request;
        var url = $"{request.Scheme}://{request.Host}";
        var urlArchivo = Path.Combine(url, contenedor, nombreArchivo).Replace("\\", "/");

        return urlArchivo;
    }

    public Task Borrar(string? ruta, string contenedor)
    {
        if(string.IsNullOrWhiteSpace(ruta))
        {
            return Task.CompletedTask;
        }

        var nombreArchivo = Path.GetFileName(ruta);
        var directorioArchivo = Path.Combine(env.WebRootPath, contenedor, nombreArchivo);

        if( File.Exists(directorioArchivo) )
        {
            File.Delete(directorioArchivo);
        }

        return Task.CompletedTask;
    }
}
