using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        
        void Delete(TEntity entity, bool softDelete = true);

       
        void Delete(int id);

        // Updates an existing entity in the repository
        void Update(TEntity entity);

        // Retrieves an entity by the specified ID
        TEntity GetById(int id);

        // Retrieves an entity that matches the specified criteria
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        // Retrieves all entities that match the specified criteria (if any). 
        // If no criteria is provided, retrieves all entities.
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
    }
}