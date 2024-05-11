using AutoMapper;
using Datos;
using Datos.DTO;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace APITareasPendientes
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<TareaDTO, Tarea>();
        }
    }
}
