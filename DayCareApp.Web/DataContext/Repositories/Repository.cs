using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace DayCareApp.Web.DataContext.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public TEntity Get(int? id)
        {
            // Here we are working with a DbContext, not PlutoContext. So we don’t have DbSets
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            List<TEntity> list;
            
            
                IQueryable<TEntity> dbQuery = Context.Set<TEntity>();

                //Apply eager loading
                foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

                list = dbQuery.AsNoTracking().ToList<TEntity>();
            
            return list;


            //return Context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] navigationProperties)
        {

            List<TEntity> list;
            
                IQueryable<TEntity> dbQuery = Context.Set<TEntity>();

                //Apply eager loading
                foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

                list = dbQuery.AsNoTracking().Where(where).ToList<TEntity>();
            
            return list;
            //return Context.Set<TEntity>().Where(predicate);
        }

        public TEntity SingleOrDefault(Func<TEntity, bool> where,params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            TEntity item = null;
            
                IQueryable<TEntity> dbQuery = Context.Set<TEntity>();

                //Apply eager loading
                foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

                item = dbQuery
                    .AsNoTracking() //Don't track any changes for the selected item
                    .FirstOrDefault(where); //Apply where clause
            
            return item;

            //return Context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Edit(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }
    }
}