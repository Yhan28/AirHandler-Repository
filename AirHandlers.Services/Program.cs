using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using AirHandlers.Contracts.AirHandlers;
using AirHandlers.Contracts.AirHandlersRecipe;
using AirHandlers.Contracts.Recipes;
using AirHandlers.Data.Repositories;
using AirHandlers.Contracts;
using AirHandlers.Data;
using AirHandlers.Services.Services;
using AirHandlers.Services.IServices;
using System.Reflection.Metadata;
using AirHandlers.DataAccess.Contexts;

namespace AirHandlers.Services
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Additional configuration is required to successfully run gRPC on macOS.
            // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

            // Add services to the container.
            builder.Services.AddGrpc();
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            builder.Services.AddMediatR(new MediatRServiceConfiguration()
            {
                AutoRegisterRequestProcessors = true,
            }
            .RegisterServicesFromAssemblies(typeof(AssemblyReference).Assembly));

            builder.Services.AddSingleton("Data Source=Data.sqlite");
            builder.Services.AddScoped<ApplicationDbContext>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAirHandlerRepository, AirHandlerRepository>();
            builder.Services.AddScoped<IRoomRepository, RoomRepository>();
            builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();


            //builder.Services.AddScoped<IPriceRepository, ApplicationRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<AirHandlerService>();
            app.MapGrpcService<RoomService>();
            app.MapGrpcService<RecipeService>();

            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}


