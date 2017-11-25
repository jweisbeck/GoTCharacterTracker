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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
