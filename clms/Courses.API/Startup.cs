using System;
using System.IO;
using System.Reflection;
using Courses.API.Common;
using Courses.API.Context;
using Courses.API.Helpers;
using Courses.API.Models;
using Courses.API.Repository.Read;
using Courses.API.Repository.Write;
using Courses.API.Services;
using Courses.API.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Courses.API
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
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CoursesDatabase")));

            services.AddTransient<IReadCourseRepository, ReadCourseRepository>();
            services.AddTransient<IWriteCourseRepository, WriteCourseRepository>();
            services.AddTransient<IReadResourceFileRepository, ReadResourceFileRepository>();
            services.AddTransient<IWriteResourceFileRepository, WriteResourceFileRepository>();

            services.AddTransient<IMapper<Course, CourseDto, CourseCreateDto>, CourseMapper>();
            services.AddTransient<IMapper<ResourceFile, ResourceFileDto, ResourceFileCreateDto>, ResourceFileMapper>();

            services.AddTransient<IValidator<Course>, CourseValidator>();
            services.AddTransient<IValidator<ResourceFile>, ResourceFileValidator>();

            services.AddTransient<IFileStorageService, FileStorageService>();


            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Courses HTTP API",
                    Version = "v1",
                    Description = "The Courses and Labs microservice HTTP API",
                    TermsOfService = "Terms Of Service"
                });
                options.OperationFilter<AddRequiredHeaderParameter>();
                options.OperationFilter<FormFileSwaggerFilter>();

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

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Courses.API"); });
            app.UseCoursesMiddleware();
            app.UseMvc();
        }
    }
}
