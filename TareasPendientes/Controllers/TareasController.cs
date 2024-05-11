using AutoMapper;
using Datos;
using Datos.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace TareasPendientes.Controllers
{
    [ApiController]
    [Route("api/tareasPendientes")]
    public class TareasController : Controller
    {
        private readonly Context _dataContext;
        private readonly IMapper _mapper;

        public TareasController(Context _db, IMapper _mp)
        {
            this._dataContext = _db;
            this._mapper = _mp;
        }

        [HttpGet]
        [Route("ListarTareas")]
        public async Task<ActionResult> ListarTareas()
        {
            
            var response = await _dataContext.Tareas.ToListAsync();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> GuardarTarea(TareaDTO dto)
        {
            var response = _mapper.Map<Tarea>(dto);
            _dataContext.Add(response);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}
