using System;
using System.Collections.Generic;
using System.Linq;

namespace Conversation.ServiceHost.Internal.Infrastructure.Repository.Abstractions
{ 
    public interface INoSqlDataRepository<T> where T : class, new()
    {
        void Index(T document);
        void Remove(T document);
        void Remove(string id);
        void Remove(string id, string routingId);
        void Update(T document);
        T Get(string id);
        IEnumerable<T> GetDocuments(string[] documentIds);
    }

    public interface INoSqlEntity
    {
        string Id { get; }
        string ParentId { get; }
        string GrandParentId { get; }
    }
}