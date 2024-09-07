using System;

namespace PeliculasAPI;

public class EjemploTiempoDeVidaServicios
{

    public class ServicioTransient // nueva instancia
    {
        private readonly Guid _id;
        public ServicioTransient()
        {
            _id = Guid.NewGuid();
        }

        public Guid GetId()
        {
            return _id;
        }
    }
    public class ServicioScope // solo en el navegador
    {
        private readonly Guid _id;
        public ServicioScope()
        {
            _id = Guid.NewGuid();
        }

        public Guid GetId()
        {
            return _id;
        }
    }
    public class ServicioSingleton // toda la aplicacion
    {
        private readonly Guid _id;
        public ServicioSingleton()
        {
            _id = Guid.NewGuid();
        }

        public Guid GetId()
        {
            return _id;
        }
    }

}
