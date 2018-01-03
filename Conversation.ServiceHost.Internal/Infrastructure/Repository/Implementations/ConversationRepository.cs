using System.Collections.Generic;
using Conversation.ServiceHost.Internal.Infrastructure.Repository.Abstractions;

namespace Conversation.ServiceHost.Internal.Infrastructure.Repository.Implementations
{
    public class ConversationRepository : ElasticRepository<Model.Conversation>, IConversationRepository
    {
        private static readonly string SearchIndex = "Conversation";

        protected override void IndexDocument(Model.Conversation document)
        {
            Index(document);
        }

        protected override void UpdateDocument(Model.Conversation document)
        {
            Update(document);
        }

        public override IEnumerable<Model.Conversation> GetDocuments(string[] documentIds)
        {
            return GetAll(SearchIndex, documentIds);
        }

        protected override Model.Conversation GetDocument(string id, string parentId)
        {
            return Get(id, parentId);
        }

        public void UpdatePartial<TPartialDocument>(TPartialDocument partialDocument, string id, string parentId = null) where TPartialDocument : class
        {
            base.UpdatePartial(partialDocument, id, SearchIndex, parentId);
        }
    }
}