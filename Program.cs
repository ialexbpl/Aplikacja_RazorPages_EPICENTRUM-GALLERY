using Aplikacja_RazorPages.Data;
using Aplikacja_RazorPages.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aplikacja_RazorPages;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddSingleton<IPaintingRepository, PaintingRepository>();
        builder.Services.AddDbContext<AppDbContext>();
        builder.Services.AddSession();
      


        var app = builder.Build();

        // Ensure the database is created
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();
        // Enable session support
        app.UseSession();

        app.Run();
    }
}
