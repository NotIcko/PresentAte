namespace PresentAte
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using PresentAte.Data;
    using PresentAte.Data.Models;
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

            builder.Services.AddDefaultIdentity<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = SignInRequireConfirmedAccount;
                options.Password.RequireDigit = PasswordRequireDigit;
                options.Password.RequireLowercase = PasswordRequireLowercase;
                options.Password.RequireNonAlphanumeric = PasswordRequireNonAlphanumeric;
                options.Password.RequireUppercase = PasswordRequireUppercase;
                options.Password.RequiredLength = PasswordRequiredLength;
                options.Password.RequiredUniqueChars = PasswordRequiredUniqueChars;
            })
            .AddEntityFrameworkStores<PresentAteDbContext>();

            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
