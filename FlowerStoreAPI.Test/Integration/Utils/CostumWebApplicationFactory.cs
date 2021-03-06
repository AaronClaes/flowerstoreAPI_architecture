using System;
using System.Linq;
using BasicRestAPI.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FlowerStoreAPI.Test.Integration.Utils.CustomWebApplicationFactory
{
    // Used for integration testing, based on https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.1
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup: class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<FlowerDatabaseContext>));

                services.Remove(descriptor);

                services.AddDbContext<FlowerDatabaseContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<FlowerDatabaseContext>();
                var logger = scopedServices
                    .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                db.Database.EnsureCreated();
            });
        }

        // You can think of Action<...> as a reference to a method that is being passed.
        public void ResetAndSeedDatabase(Action<FlowerDatabaseContext> contextFiller)
        {
            // Retrieve a service scope and a database-context instance.
            using var scope = Services.CreateScope();
            var scopedServices = scope.ServiceProvider;

            var db = scopedServices.GetRequiredService<FlowerDatabaseContext>();
            // Clear the database
            db.Flowers.RemoveRange(db.Flowers.ToList());
            db.Stores.RemoveRange(db.Stores.ToList());
            db.SaveChanges();

            // execute the method using retrieved database as parameter
            contextFiller(db);

            db.SaveChanges();
        }
    }
}