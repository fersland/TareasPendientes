using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repository
{
    public interface ITareaRepository
    {
        IEnumerable<Tarea> GetAll();
        Tarea GetById(int id);
        void Insert(Tarea tarea);
        void Update(int id, Tarea tarea);
        void Delete(int id);
    }
}
