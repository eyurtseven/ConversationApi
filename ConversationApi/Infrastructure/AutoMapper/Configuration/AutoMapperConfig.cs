using AutoMapper;
using ConversationApi.Infrastructure.AutoMapper.MapperProfile;

namespace ConversationApi.Infrastructure.AutoMapper.Configuration
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappers()
        {
            return new MapperConfiguration(cfg =>
            { 
                cfg.AddProfile(new ServiceToClientMapperProfile());
            });
        }
    }
}