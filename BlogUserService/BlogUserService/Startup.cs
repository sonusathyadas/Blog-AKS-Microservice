using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using BlogUserService.Infrastructure;
using Swashbuckle.AspNetCore.Swagger;

namespace BlogUserService
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

            services.AddDbContext<BlogUserContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("UserDatabaseConnection"));
            });

            services.AddCors();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "User Management API",
                    Version = "v1",
                    Contact = new Contact
                    {
                        Name = "Sonu Sathyadas",
                        Email = "sonusathyadas@hotmail.com",
                        Url = "https://streamingskills.blog"
                    },
                    Description = "User service API for blog users",
                    TermsOfService = string.Empty,
                    License = new License { Name = "MIT License", Url = "https://streamingskills.blog/license" }
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseHsts();
            }
            app.UseCors(options =>
            {
                options.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            InitializeDatabase(app);
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "User management API");
                options.RoutePrefix = string.Empty;
            });
            //app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<BlogUserContext>().Database.Migrate();

            }
        }
    }
    
}
