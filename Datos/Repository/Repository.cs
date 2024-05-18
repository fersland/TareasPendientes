using Datos.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly Context _dataContext;
        private readonly DbSet<T> _dbSet;
            
        public Repository(Context dt)
        {
            this._dataContext = dt;
            _dbSet = _dataContext.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            //var response = _dataContext.Tareas.ToList();
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            //return _dataContext.Tareas.FirstOrDefault(t => t.Id == id);
            return _dbSet.Find(id);
        }

        public void Insert(T dto)
        {
            // Validando la entrada de datos
            if(dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            _dataContext.Add(dto);
            _dataContext.SaveChanges();
        }

        public void Update(int id, T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var existingData = GetById(id);
            if (existingData == null)
            {
                throw new Exception("Entidad no encontrada.");
            }

            _dataContext.Entry(existingData).CurrentValues.SetValues(entity);
            _dataContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                if (_dataContext.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Remove(entity);
                _dataContext.SaveChanges();
            }
        }
    }
}
