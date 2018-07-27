using System;
using System.Diagnostics;
using BookLibrary.App.Filters;
using BookLibrary.Data;
using BookLibrary.ServiceLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookLibrary.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<BookLibraryAppContext>(options => 
            {
                    string connectionString = this.Configuration.GetConnectionString("BookLibrary");
                    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("BookLibrary.App"));
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(40);
                options.Cookie.HttpOnly = true;
            });

            services.AddMvc(options => 
            {
                options.Filters.Add<LogExecution>();
                options.Filters.Add<ExceptionFilter>();
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<BooksService>();
            services.AddTransient<AuthorsService>();
            services.AddTransient<DirectorsService>();
            services.AddTransient<MoviesService>();
            services.AddTransient<BorrowersService>();
            services.AddTransient<UsersService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<Stopwatch>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseMvcWithDefaultRoute();
        }
    }
}
