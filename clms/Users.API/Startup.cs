using System;
using System.IO;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.API.Context;
using Users.API.Helpers;
using Users.API.Models;
using Users.API.Repository.Read;
using Users.API.Repository.Write;

namespace Users.API
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

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=clms;Trusted_Connection=True;"));
            //            services.AddDbContext<ApplicationContext>(options => options.UseMySql(@"server=fenrir.info.uaic.ro;uid=clmsusers;pwd=QEtCDIZR6t;database=clmsusers"));

            services.AddTransient<IReadRepository<User>, ReadUserRepository>();
            services.AddTransient<IReadRepository<Student>, ReadStudentRepository>();
            services.AddTransient<IReadRepository<Teacher>, ReadTeacherRepository>();

            services.AddTransient<IWriteRepository<User>, WriteUserRepository>();
//            services.AddTransient<IWriteRepository<Student>, WriteStudentRepository>();
            services.AddTransient<IWriteRepository<Teacher>, WriteTeacherRepository>();

            services.AddTransient<IMapper<User, UserDto>, Mapper2<User, UserDto>>();
            services.AddTransient<IMapper<User, UserCreateDto>, Mapper2<User, UserCreateDto>>();

            services.AddTransient<IValidator<User>, UserValidator>();
            services.AddTransient<IValidator<Student>, StudentValidator>();

            // Add framework services.
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Users HTTP API",
                    Version = "v1",
                    Description = "The Users Microservice HTTP API",
                    TermsOfService = "Terms Of Service"
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services
                .AddMvc()
                .AddFluentValidation(fvc =>
                    fvc.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger()
                .UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Users.API"); });

            app.UseMvc();
        }
    }
}