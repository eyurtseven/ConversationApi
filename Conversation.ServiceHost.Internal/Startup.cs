using System.IO;
using Conversation.ServiceHost.Internal.Infrastructure.Repository.Abstractions;
using Conversation.ServiceHost.Internal.Infrastructure.Repository.Implementations;
using Conversation.ServiceHost.Internal.Services.Abstractions;
using Conversation.ServiceHost.Internal.Services.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace Conversation.ServiceHost.Internal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Conversation Internal API", Version = "v1" });
                
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Conversation.ServiceHost.Internal.xml");
                c.IncludeXmlComments(filePath);
            });
            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Conversation Internal API V1");
            });
        }
        
        private void RegisterServices(IServiceCollection services)
        { 
            services.AddScoped<IConversationRepository, ConversationRepository>(); 
            services.AddScoped<IConversationService, ConversationService>();
        }
    }
}