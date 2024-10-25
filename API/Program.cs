using Core;
using Core.Entities;
using Core.MappingProfiles;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Data;
using Serilog;
using Service;

namespace API;

public class Program
{
    public static async Task Main(string[] args)
    {
        //Log.Logger = new LoggerConfiguration()
        //  .MinimumLevel.Information()
        //  .WriteTo.Console()
        //  .WriteTo.File("logs/log.txt",
        //  rollingInterval: RollingInterval.Day,
        //  retainedFileCountLimit: 30)
        //  .CreateLogger();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build())
            .CreateLogger();

        try
        {
            Log.Information("Starting the application");

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();
            #region Services Settings

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("sqlserver"));
            });

            builder.Services.AddAutoMapper(m => m.AddProfile(new BookProfile()));
            builder.Services.AddAutoMapper(m => m.AddProfile(new CategoryProfile()));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IService<Book>, BookService>();
            builder.Services.AddScoped<IService<Category>, CategoryService>();

            builder.Services.AddLogging();
            #endregion

            var app = builder.Build();

            #region Apply all migrations & Data Seeding

            var service = app.Services.CreateScope().ServiceProvider;
            var context = service.GetRequiredService<AppDbContext>();
            var logger = service.GetRequiredService<ILogger<Program>>();

            try
            {
                logger.LogInformation("Applying migrations...");
                await context.Database.MigrateAsync();
                logger.LogInformation("Seeding data...");
                await DataSeeder.SeedDataAsync(context);
                logger.LogInformation("Data seeding completed successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while applying migrations and seeding data.");
            }

            #endregion

            #region Middlewares

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed");
        }
        finally
        {
            Log.CloseAndFlush();
        }

    }
}
