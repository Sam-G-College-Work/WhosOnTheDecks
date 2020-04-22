using System;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WhosOnTheDecks.API.Data;

namespace WhosOnTheDecks.API
{
    public class Program
    {
        //Main Method to start back end program
        public static void Main(string[] args)
        {
            //CreateHostBuilder is stored as a variable host to call later in method
            var host = CreateHostBuilder(args).Build();

            //Injecting the database into the main method is not possible
            //Instead an instance will be created within the using statment
            //so the instance will be disposed of once data is seeded
            using (var scope = host.Services.CreateScope())
            {
                //property services is created to query the scope instance from above
                var services = scope.ServiceProvider;

                //Try catch is used to defer any issues from occuring
                //and loggin the issues if one occurs
                //during migration
                try
                {
                    //Data context is called and stored as context
                    var context = services.GetRequiredService<DataContext>();

                    //This will check to see if any recent migrations have been applied
                    //but not added tot he databse
                    //following this check the database will be created
                    context.Database.Migrate();

                    //Data seed method is called and applied to the database
                    Seed.SeedData(context);
                }
                catch (Exception ex)
                {
                    //Logger property is created to store instance of logger service
                    var logger = services.GetRequiredService<ILogger<Program>>();

                    //Logger then stores the error upon catch
                    logger.LogError(ex, "An error occured during migration");
                }
            }
            //CreateHostBuilder is then told to run
            host.Run();
        }

        // Configures settings before startup of back end API
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
