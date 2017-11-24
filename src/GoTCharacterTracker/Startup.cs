using System;
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

namespace GoTCharacterTracker
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            m_configuration = configuration;
        }

        public IConfiguration m_configuration { get; set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();
            services.AddMvcCore();
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
