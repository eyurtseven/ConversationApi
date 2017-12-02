using System;

namespace ConversationApi.Infrastructure.Repository.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
    }
}