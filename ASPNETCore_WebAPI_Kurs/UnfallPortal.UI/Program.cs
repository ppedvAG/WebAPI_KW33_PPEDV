﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UnfallPortal.UI.Data;
using UnfallPortal.UI.Services;

namespace UnfallPortal.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<TempContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("TempContext") ?? throw new InvalidOperationException("Connection string 'TempContext' not found."));
                options.UseLazyLoadingProxies();
            });
               
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient<IMandantService, MandantService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}