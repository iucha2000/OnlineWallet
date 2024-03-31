
using OnlineWallet.Application;
using OnlineWallet.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineWallet.Domain.Abstractions.Interfaces;
using OnlineWallet.Infrastructure.Persistence;
using OnlineWallet.WebApi.Extensions;
using System.Reflection;

namespace OnlineWallet.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddUILayer();
            builder.Services.AddApplicationLayer();
            builder.Services.AddInfrastructureLayer(builder.Configuration);      

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
