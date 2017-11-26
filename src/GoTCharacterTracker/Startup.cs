using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using GoTCharacterTracker.Data.Services;
using GoTCharacterTracker.Data.Managers;
using GoTCharacterTracker.Data.Repository;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using GoTCharacterTracker.Data.DTO.ExceptionHandlers;
using System.Net;

namespace GoTCharacterTracker
{
    public class Startup
    {
        private string m_securityKey;

        public Startup(IConfiguration configuration)
        {
            m_configuration = configuration;
            m_securityKey = m_configuration["JWTSecurityKey"];
        }

        public IConfiguration m_configuration { get; set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();
            services.AddMvcCore();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = "localhost",
                       ValidAudience = "localhost",
                       IssuerSigningKey = new SymmetricSecurityKey(
                           Encoding.UTF8.GetBytes(m_securityKey))
                   };
               });
            services.AddTransient<ICharacterService, CharacterService>();
            services.AddTransient<ICharacterManager, CharacterManager>();
            services.AddSingleton<IConfiguration>(m_configuration);  
            services.AddTransient<IDbContext, DbContext>();  


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {


             app.UseExceptionHandler(options => {
                 options.Run(
                 async context =>
                 {
                     context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                     context.Response.ContentType = "application/json";
                     var ex = context.Features.Get<IExceptionHandlerFeature>();
                     if (ex != null)
                     {
                         var stackTraceError = string.Empty;
                        if (env.IsDevelopment())
                         {
                             stackTraceError = ex.Error.StackTrace;
                         }

                         var err = JsonConvert.SerializeObject(new ErrorMiddlewareDTO()
                         {
                            Message = ex.Error.Message,
                            Stacktrace = stackTraceError
                         });
                         await context.Response.Body.WriteAsync(Encoding.ASCII.GetBytes(err), 0, err.Length).ConfigureAwait(false);
                     }
                 });
            });

            app.UseMvc();


        }
    }
}
