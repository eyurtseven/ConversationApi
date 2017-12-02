using System.Linq;

namespace ConversationApi.Common.ResponseHelper
{
    public class PaginationResponse<T> where T : class
    {
        public PaginationResponse()
        {
        }
        public IQueryable<T> Items { get; set; }
        public long Total { get; set; }
    }
}