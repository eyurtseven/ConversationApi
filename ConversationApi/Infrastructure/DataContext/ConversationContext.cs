using ConversationApi.Model;
using Microsoft.EntityFrameworkCore;

namespace ConversationApi.Infrastructure.DataContext
{
    public class ConversationContext : DbContext
    {
        public ConversationContext(DbContextOptions<ConversationContext> options) 
            : base(options)
        {
        }

        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ConversationMessage> ConversationMessages { get; set; }
        
    }
}