using System;
using System.Collections.Generic;
using AuthenticationService.Models;
using CategoryService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Xunit;

namespace InfraSetup
{
    //WebApplication Factory for Category API
    public class CategoryWebApplicationFactory<TStartup> : WebApplicationFactory<CategoryService.Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<CategoryContext>();

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<CategoryContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CategoryWebApplicationFactory<TStartup>>>();

                    try
                    {
                        // Seed the database with some specific test data.
                        context.Categories.DeleteMany(Builders<CategoryService.Models.Category>.Filter.Empty);
                        context.Categories.InsertMany(new List<CategoryService.Models.Category>
            {
                new CategoryService.Models.Category{Id=101, Name="Sports", CreatedBy="Mukesh", Description="All about sports", CreationDate=DateTime.Now },
                 new CategoryService.Models.Category{Id=102, Name="Politics", CreatedBy="Mukesh", Description="INDIAN politics", CreationDate=DateTime.Now }
            });
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }
    }

    //WebApplication Factory for Authentication API
    public class AuthWebApplicationFactory<TStartup> : WebApplicationFactory<AuthenticationService.Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                // Add a database context (KeepNoteContext) using an in-memory database for testing.
                services.AddDbContext<AuthDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryAuthDB");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var authDb = scopedServices.GetRequiredService<AuthDbContext>();

                    var logger = scopedServices.GetRequiredService<ILogger<AuthWebApplicationFactory<TStartup>>>();

                    // Ensure the database is created.
                    authDb.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with some specific test data.
                        authDb.Users.Add(new AuthenticationService.Models.User {UserId="Mukesh", Password="admin123" });
                        authDb.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }
    }

    [CollectionDefinition("Auth API")]
    public class DbCollection : ICollectionFixture<AuthWebApplicationFactory<AuthenticationService.Startup>>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
