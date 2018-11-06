using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogService.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging; 
 using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using BlogService.Repositories;
using BlogService.Models;

namespace BlogService
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer= Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    RequireSignedTokens = false
                };
            });

            services.AddDbContext<BlogContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("BlogDatabaseConnection"));
            });

            services.AddCors();

            services.AddTransient<IBlogRepository<Blog>, BlogRepository<Blog>>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "Blog API",
                    Version = "v1",
                    Contact = new Contact
                    {
                        Name = "Sonu Sathyadas",
                        Email = "sonusathyadas@hotmail.com",
                        Url = "https://streamingskills.blog"
                    },
                    Description = "Blog API to manage the blogs posted by users",
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

            //app.UseHttpsRedirection();
            app.UseCors(options =>
            {
                options.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog API");
                options.RoutePrefix = string.Empty;
            });
            InitializeDatabase(app);
            app.UseAuthentication();
            app.UseMvc();
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<BlogContext>().Database.Migrate();

            }
        }
    }
}
