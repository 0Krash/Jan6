using Jan6.Database;
using Microsoft.EntityFrameworkCore;

namespace Jan6.Models;

public static class prepDB
{
    public static void PrepPopulation(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<UserDbContext>());
        }
    }

    public static void SeedData(UserDbContext context)
    {

        // System.Console.WriteLine("Appling Migrations...");
        // context.Database.Migrate();
        if (context.Users.Any())
        {
            System.Console.WriteLine("Appling Migrations...");
            context.Database.Migrate();
        }
    }
}

