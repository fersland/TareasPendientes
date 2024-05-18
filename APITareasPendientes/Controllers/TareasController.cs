using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Datos;
using Datos.DTO;
using Datos.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime;
using System.Threading;
using System.Xml;

namespace APITareasPendientes.Controllers
{
    [ApiController]
    [Route("api/tareasPendientes")]
    public class TareasController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Tarea> _repository;

        public TareasController(IRepository<Tarea> _rp, IMapper _mp)
        {
            this._repository = _rp;
            this._mapper = _mp;
        }

        [HttpGet]
        [Route("ListarTareas")]
        public ActionResult<IEnumerable<Tarea>> ListarTareas()
        {
            IEnumerable<Tarea> tareas;
            try
            {
                tareas = _repository.GetAll();
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(tareas);
        }
                
        [HttpPost]
        [Route("GuardarTareas")]
        public ActionResult GuardarTarea(TareaDTO dto)
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

            _repository.Insert(tarea);
            return Ok();
        }        

        [HttpGet("VerTarea")]
        public ActionResult VerTarea(int id)
        {
            Tarea tarea = new Tarea();
            try
            {
                tarea = _repository.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(tarea);
        }


        // Acción para procesar los datos del formulario de edición
        [HttpPut("EditarTarea")]    
        public IActionResult Edit(int id, [FromBody] Tarea entity)
        {
            if (entity == null)
            {
                return BadRequest("La entidad no puede ser nula.");
            }            

            if (id != entity.Id)
            {
                // Registrar los IDs para ver cuál es el problema
                Console.WriteLine($"ID de la URL: {id}, ID de la entidad: {entity.Id}");
                return BadRequest("El ID de la URL no coincide con el ID de la entidad.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = _repository.GetById(id);
                if (response == null)
                {
                    return NotFound();
                }
                _repository.Update(id, entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar la entidad: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }

            _repository.Delete(id);
            return Ok();
        }

    }
}
