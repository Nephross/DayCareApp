using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace DayCareApp.Web.DataContext.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int? id);
        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties);
        IEnumerable<TEntity> Find(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] navigationProperties);
        TEntity SingleOrDefault(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] navigationProperties);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Edit(TEntity entity);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}