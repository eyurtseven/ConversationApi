namespace Conversation.ServiceHost.Internal.Infrastructure.Repository.Abstractions
{
    public interface IElasticsearchDataRepository<T> 
    {
        void UpdatePartial<TPartialDocument>(TPartialDocument partialDocument, string id, string parentId = null) where TPartialDocument : class;
        T Get(string id, string parentId);
    }
}