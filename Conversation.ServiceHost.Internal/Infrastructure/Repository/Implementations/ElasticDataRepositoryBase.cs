using System;
using System.Collections.Generic;
using Conversation.ServiceHost.Internal.Infrastructure.Elasticsearch;
using Conversation.ServiceHost.Internal.Infrastructure.Repository.Abstractions;
using Elasticsearch.Net;
using Nest;

namespace Conversation.ServiceHost.Internal.Infrastructure.Repository.Implementations
{
    public abstract class ElasticDataRepositoryBase<TDocument> : INoSqlDataRepository<TDocument>
        where TDocument : class, INoSqlEntity, new()
    {
        private readonly ElasticClient _esClient = null;

        protected ElasticDataRepositoryBase()
        {
            _esClient = ElasticClientFactory.Create();
        }
        
        public void Index(TDocument document)
        {
            var elasticSearchTypeAttribute = Attribute.GetCustomAttribute(typeof(TDocument), typeof(ElasticsearchTypeAttribute));
            var elasticSearchTypeName = ((ElasticsearchTypeAttribute) elasticSearchTypeAttribute).Name;

            var request = new IndexRequest<TDocument>(document, _esClient.ConnectionSettings.DefaultIndex,
                elasticSearchTypeName)
            {
                Parent = document.ParentId,
                Routing = document.ParentId,
                Refresh = Refresh.True
            };

            var status = _esClient.Index(request);

            ElasticsearchExceptionBuilder(status, ElasticsearchOperation.Index);
        }

        public void Remove(TDocument document)
        {
            IDeleteResponse response;

            if (document.ParentId != null)
            {
                response = _esClient.Delete(DocumentPath<TDocument>.Id(document.Id), g => g.Parent(document.ParentId));
            }
            else
            {
                response = _esClient.Delete(DocumentPath<TDocument>.Id(document.Id));
            }

            ElasticsearchExceptionBuilder(response, ElasticsearchOperation.Delete);
        }


        public void Remove(string id)
        {
            var status = _esClient.Delete(DocumentPath<TDocument>.Id(id));
            ElasticsearchExceptionBuilder(status, ElasticsearchOperation.Delete);
        }

        public void Remove(string id, string routingId)
        {
            var status = _esClient.Delete(DocumentPath<TDocument>.Id(id), p => p.Routing(routingId));
            ElasticsearchExceptionBuilder(status, ElasticsearchOperation.Delete);
        }

        public void Update(TDocument document)
        {
            UpdateRequest<TDocument, TDocument> request = null;

            var elasticSearchTypeAttribute =
                Attribute.GetCustomAttribute(typeof(TDocument), typeof(ElasticsearchTypeAttribute));
            var elasticSearchTypeName = ((Nest.ElasticsearchTypeAttribute) elasticSearchTypeAttribute).Name;

            if (document.ParentId != null)
            {
                request = new UpdateRequest<TDocument, TDocument>(document, _esClient.ConnectionSettings.DefaultIndex,
                    elasticSearchTypeName)
                {
                    Routing = document.ParentId,
                    Parent = document.ParentId,
                    Doc = document
                };
            }
            else
            {
                request = new UpdateRequest<TDocument, TDocument>(document, _esClient.ConnectionSettings.DefaultIndex,
                    elasticSearchTypeName)
                {
                    Doc = document,
                    Refresh = Refresh.True
                };
            }

            var status = _esClient.Update<TDocument>(request);

            ElasticsearchExceptionBuilder(status, ElasticsearchOperation.Update);
        }

        public TDocument Get(string id)
        {
            return _esClient.Get(DocumentPath<TDocument>.Id(id)).Source;
        }

        public abstract IEnumerable<TDocument> GetDocuments(string[] documentIds);

        private static void ElasticsearchExceptionBuilder(IResponse status, ElasticsearchOperation operation)
        {
            string op = string.Empty;
            switch (operation)
            {
                case ElasticsearchOperation.Index:
                    op = "indexing";
                    break;
                case ElasticsearchOperation.Update:
                    op = "updating";
                    break;
                case ElasticsearchOperation.Delete:
                    op = "deleting";
                    break;
                default:
                    break;
            }

            var error = status.ServerError;
            if (error?.Error != null)
            {
                throw new Exception("An error occured when " + op + " in elasticsearch : " + Environment.NewLine
                                    + "Error : " + error.Error + Environment.NewLine
                                    + "Status : " + error.Status);
            }
        }
    }

    public enum ElasticsearchOperation
    {
        Index,
        Update,
        Delete
    }
}