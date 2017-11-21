using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GoTCharacterTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var defaultBuilder = WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(ConfigConfiguration)
            .UseStartup<Startup>()
            .Build();

            return defaultBuilder;
        }

        static void ConfigConfiguration(WebHostBuilderContext webHostBuilderContext, IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();

            var config = configurationBuilder.Build();

            configurationBuilder.AddAzureKeyVault(
                $"https://{config["vault"]}.vault.azure.net/",
                config["ClientId"],
                config["ClientSecret"]
            );
        }
    }
}
