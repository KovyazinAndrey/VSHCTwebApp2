using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI;
using VSHCTwebApp.Components;
using VSHCTwebApp.Components.Account;
using VSHCTwebApp.Components.Services;
using VSHCTwebApp.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace VSHCTwebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContextFactory<VSHCTwebAppContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("VSHCTwebAppContext") ?? throw new InvalidOperationException("Connection string 'VSHCTwebAppContext' not found.")));

            builder.Services.AddQuickGridEntityFrameworkAdapter();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            builder.Services.AddScoped<LikeService> ();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.SlidingExpiration = true;
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LogoutPath = "/";  // Кастомный путь
                options.LoginPath = "/Account/Login";    // Чтобы не было /Identity/Account/Login
            });

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(connectionString),
                ServiceLifetime.Scoped);

            builder.Services.AddDbContextFactory<ApplicationDbContext>(
                options => options.UseSqlServer(connectionString),
                ServiceLifetime.Scoped);

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ClientOnly", policy => policy.RequireRole("ClientOnly"));
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ClientOrAdmin", policy =>
                    policy.RequireAssertion(context =>
                        context.User.IsInRole("Client") || context.User.IsInRole("Admin")));
            });


            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDbContextFactory<VSHCTwebAppContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("VSHCTwebAppContext") ??
                    throw new InvalidOperationException(
            "Connection string 'VSHCTwebAppContext' not found.")));


            builder.Services.AddQuickGridEntityFrameworkAdapter();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            builder.Services.AddSingleton<CommandService>();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
    app.UseMigrationsEndPoint();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();

            app.Run();
        }
    }
}
