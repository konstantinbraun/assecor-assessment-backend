using Assecor.Api.Factory;
using Assecor.DAL.Data;
using Assecor.DAL.Interfaces;
using Assecor.DAL.LineBuilder;
using Assecor.DAL.Readers;
using Assecor.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Assecor.DAL.Writers;

namespace Assecor.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions((options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            }));

            services.AddDbContext<PersonContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            services.AddSingleton<ILineReader, CsvLineBuilder>();
            services.AddSingleton<ILineWriter, CsvLineBuilder>();

            services.AddSingleton<IReader, PersonReader>();
            services.AddSingleton<IWriter, PersonWriter>();

            services.AddScoped<IPersonRepository, DbPersonRepository>();

            services.AddScoped<IRepositoryFactory, RepositoryFactory>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Person API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            
            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
