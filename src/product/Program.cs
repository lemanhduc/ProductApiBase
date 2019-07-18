using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pivotal.Extensions.Configuration.ConfigServer;
using product;
using Steeltoe.Extensions.Logging;

namespace mississippi.product {
    public class Program {
        public static void Main (string[] args) {
            var builder = CreateWebHostBuilder (args).Build ();
            using (var scope = builder.Services.CreateScope ()) {
                var services = scope.ServiceProvider;
                var dbContext = services.GetService<ProductDBContext> ();
                
                Console.WriteLine ("Applying database migrations");
                dbContext.Database.Migrate ();

                Console.WriteLine ("Seeding the database");
                new ProductDBContextSeed ()
                    .SeedAsync (dbContext)
                    .Wait ();
            }
            builder.Run ();
        }

        public static IWebHostBuilder CreateWebHostBuilder (string[] args) =>
            WebHost.CreateDefaultBuilder (args)
            .UseCloudFoundryHosting ()
            .ConfigureLogging ((builderContext, loggingBuilder) => {
                loggingBuilder.ClearProviders ();
                loggingBuilder.AddConfiguration (builderContext.Configuration.GetSection ("Logging"));
                loggingBuilder.AddDynamicConsole ();
            })
            .UseStartup<Startup> ();
    }
}