using Jan6.Models;
using Jan6.Database;
using Microsoft.EntityFrameworkCore;

namespace Jan6;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // var server = Configuration["ServerName"] ?? "sqlServer";
        // var port = Configuration["Port"] ?? "1433";
        // var database = Configuration["Database"] ?? "JanDb";
        // var user = Configuration["UserName"] ?? "SA";
        // var password = Configuration["Password"] ?? "mF!00xK#";

        services.AddControllers();
        services.AddDbContext<UserDbContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection"));
            // options.UseSqlServer($"Server={server},{port}; Initial Catalog={database}; User={user}; Password={password}; TrustServerCertificate=true");
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllers());

        prepDB.PrepPopulation(app);
    }
}