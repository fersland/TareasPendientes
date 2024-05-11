using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Datos;
using Datos.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APITareasPendientes.Controllers
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
        [Route("GuardarTareas")]
        public async Task<ActionResult> GuardarTarea(TareaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (dto.Titulo.Contains("<script>") || dto.Titulo.Contains("<") || dto.Titulo.Contains(">"))
            {
                return BadRequest("El título contiene contenido no permitido.");
            }

            if (dto.Descripcion.Contains("<script>") || dto.Descripcion.Contains("<") || dto.Descripcion.Contains(">"))
            {
                return BadRequest("La descripcion contiene contenido no permitido.");
            }

            if (dto.Completada.Contains("<script>") || dto.Completada.Contains("<") || dto.Completada.Contains(">"))
            {
                return BadRequest("El campo completado contiene contenido no permitido.");
            }

            var tarea = _mapper.Map<Tarea>(dto);

            _dataContext.Add(tarea);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("VerTarea")]
        public async Task<ActionResult> VerTarea(int id)
        {
            var response = await _dataContext.Tareas.Where(t => t.Id == id).ToListAsync();
            return Ok(response);
        }

        [HttpPut("EditarTarea")]
        public async Task<IActionResult> EditarTarea(int id, TareaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (dto.Titulo.Contains("<script>") || dto.Titulo.Contains("<") || dto.Titulo.Contains(">"))
            {
                return BadRequest("El título contiene contenido no permitido.");
            }

            if (dto.Descripcion.Contains("<script>") || dto.Descripcion.Contains("<") || dto.Descripcion.Contains(">"))
            {
                return BadRequest("La descripcion contiene contenido no permitido.");
            }

            if (dto.Completada.Contains("<script>") || dto.Completada.Contains("<") || dto.Completada.Contains(">"))
            {
                return BadRequest("El campo completado contiene contenido no permitido.");
            }

            var response = _mapper.Map<Tarea>(dto);
            response.Id = id;
            _dataContext.Update(response);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Tareas.ToListAsync());
        }

        [HttpDelete("EliminarTarea")]
        public async Task<IActionResult> EliminarTarea(int id)
        {
            var entityToDelete = await _dataContext.Tareas.FindAsync(id);
            if (entityToDelete != null)
            {
                _dataContext.Tareas.Remove(entityToDelete);
                await _dataContext.SaveChangesAsync();
            }

            return Ok();
        }
    }
}
