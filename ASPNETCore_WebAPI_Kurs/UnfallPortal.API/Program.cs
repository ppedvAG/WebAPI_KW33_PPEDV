using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UnfallPortal.API.Data;
namespace UnfallPortal.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<UnfallPortalDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("UnfallPortalDbContext") ?? throw new InvalidOperationException("Connection string 'UnfallPortalDbContext' not found.")));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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