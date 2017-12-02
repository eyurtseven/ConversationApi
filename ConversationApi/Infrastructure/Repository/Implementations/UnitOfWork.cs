using ConversationApi.Infrastructure.DataContext;
using ConversationApi.Infrastructure.Repository.Abstractions;

namespace ConversationApi.Infrastructure.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ConversationContext _context;

        public UnitOfWork(ConversationContext context)
        {
            _context = context;
        }

        public int Commit()
        {
            return _context.SaveChanges(); 
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}