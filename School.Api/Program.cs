using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using School.Core;
using School.Core.MiddleWare;
using School.Infrastructure;
using School.Infrastructure.Data;
using School.Service;
using System.Globalization;

namespace School.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //Connection SQL
        builder.Services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext"));
        });


        #region Dependency Injections

        builder.Services
            .AddInfrastructureDependencies()
            .AddServiceDependencies()
            .AddCoreDependencies();

        #endregion


        #region Localization
        builder.Services.AddLocalization(options => options.ResourcesPath = "");

        builder.Services
            .AddControllers()
            .AddViewLocalization()
            .AddDataAnnotationsLocalization();

        var supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("en"),
            new CultureInfo("ar")
        };

        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            List<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("ar-EG")
            };
            options.DefaultRequestCulture = new RequestCulture("en-US");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });



        #endregion

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        #region Localization MiddleWare
        var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
        app.UseRequestLocalization(options.Value);
        #endregion

        // Global Exception Handler
        app.UseMiddleware<ErrorHandlerMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
