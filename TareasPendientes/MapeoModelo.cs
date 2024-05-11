using AutoMapper;
using Datos;
using Datos.DTO;

namespace TareasPendientes
{
    public class MapeoModelo : Profile
    {
        public MapeoModelo()
        {
            CreateMap<TareaDTO, Tarea>();    
        }
    }
}
