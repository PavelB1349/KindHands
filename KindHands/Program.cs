using KindHands.Data;
using KindHands.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace KindHands
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure cookie based authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        // Specify where to redirect un-authenticated users
                        options.LoginPath = "/login";

                        // Specify the name of the auth cookie.
                        // ASP.NET picks a dumb name by default.
                        options.Cookie.Name = "KindHands.Cookie";
                    });
            // Add services to the container.
            builder.Services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            ConfigureServices(builder.Services, builder.Configuration);
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
        public static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
        {
            ConfigureMvc(services, configuration);
            ConfigureDependencies(services, configuration);

        }
        private static void ConfigureDependencies(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<DefaultUserProvider>();
            services.AddSingleton<PasswordHasher>();
        }
        public static void ConfigureMvc(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<KindHandsContext>(options =>
                       options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            // Add services to the container.
            services.AddControllersWithViews();
        }
    }
}