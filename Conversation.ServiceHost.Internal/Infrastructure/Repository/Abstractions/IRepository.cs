using System;
using System.Linq;

namespace Conversation.ServiceHost.Internal.Infrastructure.Repository.Abstractions
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Add(TEntity obj);
        TEntity GetById(Guid id);
        IQueryable<TEntity> GetAll();
        TEntity Update(TEntity obj);
        void Remove(Guid id);
    }
}