using Application;
using Persistence;
using Shared;
using WebAPI.Extensions;
using Identity;
using Microsoft.Extensions.FileProviders;
using Application.Interfaces;
using WebApi.Services;
using Microsoft.OpenApi.Models;
using QuestPDF.Infrastructure;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            QuestPDF.Settings.License = LicenseType.Enterprise; // licencia del QuestPDF, no borrar, sino muere D: 

            var builder = WebApplication.CreateBuilder(args);

            var env = builder.Environment;

            Console.WriteLine(env.EnvironmentName);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .Build();

            builder.Services.AddSingleton(configuration);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            builder.Services.AddApplicationLayer();
            builder.Services.AddIdentityInfraestructure(builder.Configuration);
            builder.Services.AddSharedInfraestructure(builder.Configuration);
            builder.Services.AddPersistenceInfraestructure(builder.Configuration);
            builder.Services.AddControllers();
            builder.Services.AddApiVersioningExtension();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MATE BACKEND",
                    Version = "1.0",
                    Description = "Nuestro proyecto es una solución integral que aborda la gestión financiera y" +
                        " administrativa de tu empresa de manera eficiente y precisa. Con características clave," +
                        " como la facturación de clientes, el control de nómina, el estudio actuarial y el cumplimiento" +
                        " del NIF-D3, estamos aquí para simplificar tus operaciones.",
                    Contact = new OpenApiContact
                    {
                        Name = "Guillermo Santos",
                        Email = "gsantos@maxal.com.mx"
                    },
                    License = new OpenApiLicense
                    {
                        Name = ""
                    }
                });
            });

            // Add logging. Uncomment for use.
            //builder.Logging.AddConsole();
            //builder.Logging.AddDebug();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors("CorsPolicy");

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine("C:", @"StaticFiles")),
                RequestPath = new PathString("/StaticFiles")
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseErrorHandlingMiddleware();

            app.MapControllers();
            app.Run();
        }
    }
}
