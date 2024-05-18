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
            throw new NotImplementedException();
        }

        public void Insert(Tarea tarea)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Tarea tarea)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
