using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Backend
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddCors();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var logger = loggerFactory.CreateLogger("default");

            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
            );


            string audience = Configuration["MicrosoftIdentity:ClientId"];
            if(audience == "YOUR CLIENT ID"){
                logger.LogError("Your appsettings.json has not been updated with the client id (app id) of your application");
                Environment.Exit(1);
            }

            var tokenValidationParameters = new TokenValidationParameters();
            tokenValidationParameters.ValidateIssuer = false;

            var options = new JwtBearerOptions
            {
                Audience = Configuration["MicrosoftIdentity:ClientId"],
                Authority = Configuration["MicrosoftIdentity:Authority"],
                TokenValidationParameters = tokenValidationParameters
            };

            app.UseJwtBearerAuthentication(options);

            app.UseMvc();
        }
    }
}
