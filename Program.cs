using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace Backend_School_Project_Solo;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add services to the container.
        builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        
        builder.Services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);
        builder.Services.AddAuthorization();

        builder.Services.AddIdentityCore<UserEntity>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddControllers();
        
        // FnFMS - Folder and File Management System
        builder.Services.AddScoped<IFnFMSService, FnFMSService>();
        builder.Services.AddScoped<IFnFMSRepository, FnFMSRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(); // Access WebAPI-Testing, via URL: domain + scalar/v1 (in a browser)
        }

        app.UseHttpsRedirection();

        app.MapIdentityApi<UserEntity>();
        app.MapControllers();

        app.UseAuthentication();
        app.UseAuthorization();

        app.Run();
    }
}
