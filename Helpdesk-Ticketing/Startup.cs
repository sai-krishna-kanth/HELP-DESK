using Helpdesk_Ticketing.Data;
using Helpdesk_Ticketing.Models;
using Helpdesk_Ticketing.Models.Interfaces;
using Helpdesk_Ticketing.Models.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;


namespace Helpdesk_Ticketing
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //Configuration = configuration;
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            //builder.AddUserSecrets<Startup>();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();
            services.AddIdentity<AccountUsers, IdentityRole>()
                   //.AddEntityFrameworkStores<ApplicationDbContext>()
                   .AddDefaultTokenProviders();
            /*Local Strings*/
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration["ConnectionStrings:LocalUserConnection"]));
            services.AddDbContext<TicketsDbContext>(options =>
            options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            /*Deployment Strings*/
            //services.AddDbContext<MembersDbContext>(options =>
            //options.UseSqlServer(Configuration["ConnectionStrings:IdentityDefaultConnection"]));
            //services.AddDbContext<TicketsDbContext>(options =>
            //options.UseSqlServer(Configuration["ConnectionStrings:ProductionConnection"]));

            //services.AddScoped<ICart, CartServices>();
            //services.AddScoped<ITickets, TicketTypesServices>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole(ApplicationRoles.MemberAdmin));
            });
            //********************************
            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
