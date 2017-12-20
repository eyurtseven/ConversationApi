using System;
using Nest;

namespace Conversation.ServiceHost.Internal.Infrastructure.Elasticsearch
{
    public static class ElasticClientFactory
    {
        public static ElasticClient Create()
        {
            //TODO : Configten al
            var settings = new ConnectionSettings(new Uri("http://95.85.40.160:9200/"));
            settings.DefaultIndex("conversations");
            var client = new ElasticClient(settings);
            return client;
        }
    }
}