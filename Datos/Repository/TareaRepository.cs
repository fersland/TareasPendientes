using Datos.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repository
{
    public class TareaRepository : ITareaRepository
    {
        private readonly Context _dataContext;

        public TareaRepository(Context dt)
        {
            this._dataContext = dt;    
        }
        public IEnumerable<Tarea> GetAll()
        {
            var response = _dataContext.Tareas.ToList();
            return response;
        }

        public Tarea GetById(int id)
        {
            return _dataContext.Tareas.FirstOrDefault(t => t.Id == id);
        }

        public void Insert(Tarea dto)
        {
            // Validando la entrada de datos
            if(dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            _dataContext.Add(dto);
            _dataContext.SaveChanges();
        }

        public void Update(int id, Tarea tarea)
        {
            // Validando la entrada de datos
            if (tarea == null)
            {
                throw new ArgumentNullException(nameof(tarea));
            }

            // Obtener la tarea existente por su ID
            var existingTarea = GetById(id);

            if (existingTarea == null)
            {
                throw new Exception("Tarea no encontrada.");
            }

            existingTarea.Titulo = tarea.Titulo;
            existingTarea.Descripcion = tarea.Descripcion;
            existingTarea.FechaCreacion = tarea.FechaCreacion;
            existingTarea.FechaVencimiento = tarea.FechaVencimiento;
            existingTarea.Completada = tarea.Completada;

            //_dataContext.Update(tarea);
            _dataContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var deleteItem = _dataContext.Tareas.Find(id);
            if (deleteItem != null)
            {
                _dataContext.Tareas.Remove(deleteItem);
                _dataContext.SaveChanges();
            }
        }
    }
}
