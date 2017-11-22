﻿using System;
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

namespace GoTCharacterTracker
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {

            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(env.ContentRootPath)
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            //    .AddEnvironmentVariables();

            //var config = builder.Build();

            //builder.AddAzureKeyVault(
            //    $"https://{config["AzureVault:Name"]}.vault.azure.net/",
            //    config["AzureVault:ClientId"],
            //    config["AzureVault:ClientSecret"]
            //);

            m_configuration = configuration;
        }

        public IConfiguration m_configuration { get; set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();
            services.AddTransient<ICharacterService, CharacterService>();
            services.AddTransient<ICharacterManager, CharacterManager>();
            services.AddSingleton<IConfiguration>(m_configuration);  


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
