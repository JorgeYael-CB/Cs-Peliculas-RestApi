using AutoMapper;
using PeliculasAPI.DTos;
using PeliculasAPI.entities;

namespace PeliculasAPI.Utilidades;




public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        ConfigurarMapeoGeneros();
        ConfigurarMapeoActores();
    }

    private void ConfigurarMapeoGeneros()
    {
        CreateMap<GeneroCreacionDto, Genero>();
        CreateMap<Genero, GeneroDto> ();
    }

    private void ConfigurarMapeoActores()
    {
        CreateMap<ActorCreacionDto, Actor>()
            .ForMember( x => x.Foto,  opciones  => opciones.Ignore() );
        CreateMap<Actor, ActorDto>();
    }
}
