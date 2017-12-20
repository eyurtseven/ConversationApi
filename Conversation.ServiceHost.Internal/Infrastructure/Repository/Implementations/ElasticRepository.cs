using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Conversation.ServiceHost.Internal.Infrastructure.Elasticsearch;
using Conversation.ServiceHost.Internal.Infrastructure.Repository.Abstractions;
using Elasticsearch.Net;
using Nest;

namespace Conversation.ServiceHost.Internal.Infrastructure.Repository.Implementations
{
    public abstract class ElasticRepository<TDocument> : INoSqlDataRepository<TDocument>
        where TDocument : class, INoSqlEntity, new()
    {
        private readonly ElasticClient _esClient = null;
        QueryContainer _QueryContainer = null;

        protected ElasticRepository()
        {
            _esClient = ElasticClientFactory.Create();
        }

        protected IEnumerable<TDocument> GetAll(string searchIndex, string[] ids)
        {
            ISearchResponse<TDocument> response = GetAllExecuteInner(searchIndex, ids.Length, ids);
            return response.Documents.ToArray();
        }

        /// <summary>
        /// GetAllCreateFilteredInnerQuery method'unu override ederek istenilen property'e göre GET işlemi yapılabilir.
        /// </summary>
        /// <param name="searchIndex"></param>
        /// <param name="documentsSize">Dönecek olan document sayısı.</param>
        /// <param name="ids"></param>
        /// <returns></returns>
        protected IEnumerable<TDocument> GetAll(string searchIndex, int documentsSize, string[] ids, QueryContainer queryContainer = null)
        {
            _QueryContainer = queryContainer;

            ISearchResponse<TDocument> response = GetAllExecuteInner(searchIndex, documentsSize, ids);
            return response.Documents.ToArray();
        }

        public TDocument Get(string id)
        {
            return _esClient.Get(DocumentPath<TDocument>.Id(id)).Source;
        }

        public TDocument Get(string id, string routingId)
        {
            if (routingId != null)
            {
                var result = _esClient.Get(DocumentPath<TDocument>.Id(id), g => g.Routing(routingId));
                return result.Source;
            }
            else
            {
                var result = _esClient.Get(DocumentPath<TDocument>.Id(id));
                return result.Source;
            }
        }

        public void Index(TDocument document)
        {
            var elasticSearchTypeAttribute = Attribute.GetCustomAttribute(typeof(TDocument), typeof(Nest.ElasticsearchTypeAttribute));
            var elasticSearchTypeName = ((Nest.ElasticsearchTypeAttribute)elasticSearchTypeAttribute).Name;

            var request = new IndexRequest<TDocument>(document, _esClient.ConnectionSettings.DefaultIndex, elasticSearchTypeName)
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

            var elasticSearchTypeAttribute = Attribute.GetCustomAttribute(typeof(TDocument), typeof(ElasticsearchTypeAttribute));
            var elasticSearchTypeName = ((Nest.ElasticsearchTypeAttribute)elasticSearchTypeAttribute).Name;

            if (document.ParentId != null)
            {
                request = new UpdateRequest<TDocument, TDocument>(document, _esClient.ConnectionSettings.DefaultIndex, elasticSearchTypeName)
                {
                    Routing = document.ParentId,
                    Parent = document.ParentId,
                    Doc = document
                };
            }
            else
            {
                request = new UpdateRequest<TDocument, TDocument>(document, _esClient.ConnectionSettings.DefaultIndex, elasticSearchTypeName)
                {
                    Doc = document,
                    Refresh = Refresh.True
                };
            }

            var status = _esClient.Update<TDocument>(request);

            ElasticsearchExceptionBuilder(status, ElasticsearchOperation.Update);
        }

        protected void UpdatePartial<TPartialDocument>(TPartialDocument partialDocument, string id, string indexName, string parentId = null) where TPartialDocument : class
        {
            UpdateRequest<TDocument, TPartialDocument> request = null;

            var elasticSearchTypeAttribute = Attribute.GetCustomAttribute(typeof(TDocument), typeof(ElasticsearchTypeAttribute));
            var elasticSearchTypeName = ((Nest.ElasticsearchTypeAttribute)elasticSearchTypeAttribute).Name;

            if (parentId != null)
            {
                request = new UpdateRequest<TDocument, TPartialDocument>(indexName, elasticSearchTypeName, id)
                {
                    Routing = parentId,
                    Parent = parentId,
                    Doc = partialDocument
                };
            }
            else
            {
                request = new UpdateRequest<TDocument, TPartialDocument>(indexName, elasticSearchTypeName, id)
                {
                    Doc = partialDocument,
                    Refresh = Refresh.True
                };
            }
            
            var status = _esClient.Update<TDocument, TPartialDocument>(request);

            ElasticsearchExceptionBuilder(status, ElasticsearchOperation.Update);
        }

        protected void UpdateByQuery(string indexName, QueryContainer query, IScript script)
        {
            var elasticSearchTypeAttribute = Attribute.GetCustomAttribute(typeof(TDocument), typeof(ElasticsearchTypeAttribute));
            var elasticSearchTypeName = ((Nest.ElasticsearchTypeAttribute)elasticSearchTypeAttribute).Name;

            var request = new UpdateByQueryRequest(indexName, elasticSearchTypeName)
            {
                Query = query,
                Script = script
            };
           
            var status = _esClient.UpdateByQuery(request);
            
            ElasticsearchExceptionBuilder(status, ElasticsearchOperation.Update);
        }

        protected abstract void IndexDocument(TDocument document);

        protected abstract void UpdateDocument(TDocument document);

        public abstract IEnumerable<TDocument> GetDocuments(string[] documentIds);

        protected abstract TDocument GetDocument(string id, string parentId);

        #region Private

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

        private ISearchResponse<TDocument> GetAllExecuteInner(string searchIndex, int documentsSize, string[] ids)
        {
            var request = new SearchRequest(searchIndex, typeof(TDocument))
            {
                From = 0,
                Size = documentsSize,
                Query = GetAllCreateFilteredQuery(ids)
            };

            var result = _esClient.Search<TDocument>(request);

            string lastExecutedQueryJSon;
            using (var stream = new MemoryStream())
            {
                _esClient.Serializer.Serialize(request, stream);
                lastExecutedQueryJSon = Encoding.UTF8.GetString(stream.ToArray());
            }

            if (!result.IsValid)
            {
                //TODO: LogManager hata Idsi dondurmeye basladigi zaman, loglama islemini burada yap ve exceptionda hata mesaji olarak idyi dondur
                //
                //ILogger logger = LogManager.GetLogger();
                //logger.LogError(Elastic Query Error. Query > " + _LastExecutedQueryJSon);

                throw new Exception("Elastic Query Error. Query > " + lastExecutedQueryJSon);
            }
            return result;
        }

        private QueryContainer GetAllCreateFilteredQuery(string[] ids)
        {
            BoolQuery boolQueryBase = new BoolQuery();
            BoolQuery boolQuery = new BoolQuery();
            List<QueryContainer> boolGroup = new List<QueryContainer>();

            if (_QueryContainer == null)
            {
                _QueryContainer = new QueryContainer(new TermsQuery()
                {
                    Field = "_id",
                    Terms = ids
                });
            }

            boolGroup.Add(_QueryContainer);
            boolQuery.Must = boolGroup;
            boolQueryBase.Filter = boolGroup;

            return boolQueryBase;
        }

        #endregion
    }
}