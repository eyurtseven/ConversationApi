using System.IO;
using AutoMapper;
using ConversationApi.Common.Settings;
using ConversationApi.Infrastructure.AutoMapper.Configuration;
using ConversationApi.Infrastructure.DataContext;
using ConversationApi.Infrastructure.Repository.Abstractions;
using ConversationApi.Infrastructure.Repository.Implementations;
using ConversationApi.Services.Abstractions;
using ConversationApi.Services.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace ConversationApi
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
            
            services.AddDbContext<ConversationContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper();
            services.AddSingleton(AutoMapperConfig.RegisterMappers().CreateMapper());

            services.Configure<AppSettingsOptions>(Configuration.GetSection("AppSettings")); 
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Conversation API", Version = "v1" });
                
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "ConversationApi.xml");
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Conversation API V1");
            });
        }
        
        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ConversationContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IConversationRepository, ConversationRepository>();
            services.AddScoped<IConversationMessageRepository, ConversationMessageRepository>();
            services.AddScoped<IConversationService, ConversationService>();
        }
    }
}