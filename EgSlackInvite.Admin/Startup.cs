namespace EgSlackInvite.Admin
{
    using System.Collections.Generic;
    using System.Reflection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Autofac;
    using Autofac.Extensions.DependencyInjection;

    using CloudProvider.Abstract;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using SlackApiClient.Abstract.Service;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer Container { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("DefaultAuthorizedPolicy", policy =>
                {
                    //policy.Requirements.Add();
                    policy.AuthenticationSchemes = new List<string>
                    {
                        CookieAuthenticationDefaults.AuthenticationScheme
                    };
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var builder = new ContainerBuilder();

            var assemblies = new[]
            {
                Assembly.GetCallingAssembly(),
                Assembly.GetAssembly(typeof(IChatClientUserService)),
                Assembly.GetAssembly(typeof(IUserSettingsClient))
            };

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("Service")
                            || t.Name.EndsWith("Factory")
                            || t.Name.EndsWith("Handler")
                            || t.Name.EndsWith("Client"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();


            builder.Populate(services);

            Container = builder.Build();
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
