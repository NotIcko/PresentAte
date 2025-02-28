using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PresentAte.Data;
namespace PresentAte
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using PresentAte.Data;
    using PresentAte.Data.Models;
    using PresentAte.Services.Data.Implementations;
    using PresentAte.Services.Data.Interfaces;
    using static PresentAte.Common.ApplicationConstants.UserConstants;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<PresentAteDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = SignInRequireConfirmedAccount;
                options.Password.RequireDigit = PasswordRequireDigit;
                options.Password.RequireLowercase = PasswordRequireLowercase;
                options.Password.RequireNonAlphanumeric = PasswordRequireNonAlphanumeric;
                options.Password.RequireUppercase = PasswordRequireUppercase;
                options.Password.RequiredLength = PasswordRequiredLength;
                options.Password.RequiredUniqueChars = PasswordRequiredUniqueChars;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<PresentAteDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient<IGoogleGeminiService, GoogleGeminiService>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IGoogleGeminiService, GoogleGeminiService>();
            builder.Services.AddScoped<IPowerPointService, PowerPointService>();
            builder.Services.AddScoped<IEssayService, EssayService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=AdminPanel}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
