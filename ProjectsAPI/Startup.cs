using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Projects.API.Interfaces;
using Projects.API.Services;
using Projects.DataAccess.Interfaces;
using Projects.DataAccess.Repositories;
using Projects.Modelling.DTOs;
using Projects.Modelling.Interfaces;
using Projects.Modelling.Services;
using System.Configuration;

namespace Projects.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IEntityBinderService, EntityBinderService>();


            services.AddSingleton<IUserRepository>(x =>
                 new UserRepository(Configuration["ConnectionStrings:Users"]));

            services.AddSingleton<ITaskRepository>(x =>
                 new TaskRepository(Configuration["ConnectionStrings:Tasks"]));

            services.AddSingleton<ITeamRepository>(x =>
                 new TeamRepository(Configuration["ConnectionStrings:Teams"]));

            services.AddSingleton<IProjectRepository>(x =>
                  new ProjectRepository(Configuration["ConnectionStrings:Projects"]));

            services.AddSingleton<IUnitOfWork, APIUnitOfWork>();
            services.AddSingleton<IEntityHandlerService, EntityHandlerService>();
            services.AddSingleton<IQueryProcessingService, QueryProcessingService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectsAPI", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectsAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
