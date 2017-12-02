using AutoMapper;
using ConversationApi.Model;
using ConversationApi.Services.DTO.Response;

namespace ConversationApi.Infrastructure.AutoMapper.MapperProfile
{
    public class ServiceToClientMapperProfile : Profile
    {
        public ServiceToClientMapperProfile()
            : this("ServiceToClientMapperProfile")
        {
        }

        private ServiceToClientMapperProfile(string profileName) : base(profileName)
        {
            CreateMap<Conversation, ConversationResponse>();
        }
    }
}