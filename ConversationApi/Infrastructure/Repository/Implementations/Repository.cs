using System;
using System.Linq;
using ConversationApi.Infrastructure.DataContext;
using ConversationApi.Infrastructure.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ConversationApi.Infrastructure.Repository.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ConversationContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(ConversationContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual TEntity Add(TEntity obj)
        {
            var dbResult = DbSet.Add(obj);
            return dbResult.Entity;
        }

        public virtual TEntity GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public virtual TEntity Update(TEntity obj)
        {
            var dbResult = DbSet.Update(obj);
            return dbResult.Entity;
        }

        public virtual void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}