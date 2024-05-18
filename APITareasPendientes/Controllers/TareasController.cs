using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Datos;
using Datos.DTO;
using Datos.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime;

namespace APITareasPendientes.Controllers
{
    [ApiController]
    [Route("api/tareasPendientes")]
    public class TareasController : Controller
    {
        //private readonly Context _dataContext;
        private readonly IMapper _mapper;
        private readonly ITareaRepository _repository;

        public TareasController(ITareaRepository _rp, IMapper _mp)
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
        
        [HttpPut("EditarTarea")]
        public ActionResult EditarTarea(int id, TareaDTO dto)
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
            _repository.Update(id, response);
            return Ok();
        }


        [HttpDelete("EliminarTarea")]
        public ActionResult EliminarTarea(int id)
        {
            if(id <= 0) {
                return BadRequest("No se encontró una ID valida.");
            }

            var tareaEncontrada = _repository.GetById(id);

            if(tareaEncontrada == null)
            {
                return NotFound("No se encontró la tarea con la ID especificada");
            }

            _repository.Delete(id);
            return Ok();
        }

    }
}
