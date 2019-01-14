using System;
using System.IO;
using Gamification.API.Context;
using Gamification.API.Helpers;
using Gamification.API.Models;
using Gamification.API.Repository.Read;
using Gamification.API.Repository.Write;
using System.Reflection;
using Gamification.API.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gamification.API
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
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("GamificationDatabase")));
            services.AddTransient<IReadQuestionRepository, ReadQuestionRepository>();
            services.AddTransient<IWriteQuestionRepository, WriteQuestionRepository>();
            services.AddTransient<IReadQuestionRepository, ReadQuestionRepository>();
            services.AddTransient<IWriteQuestionRepository, WriteQuestionRepository>();

            services.AddTransient<IMapper<Question, QuestionDto, QuestionCreateDto>, QuestionMapper>();

           // services.AddTransient<IValidator<Question>, QuestionValidator>();
           

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Gamification HTTP API",
                    Version = "v1",
                    Description = "The Gamification Microservice HTTP API",
                    TermsOfService = "Terms Of Service"
                });
                options.OperationFilter<AddRequiredHeaderParameter>();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger()
                .UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gamification.API"); });

            app.UseMvc();
        }
    }
}
