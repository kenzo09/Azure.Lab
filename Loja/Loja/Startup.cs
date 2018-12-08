using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Loja.Core.Services;
using Loja.Infrastructure.Mappings;
using Loja.Infrastructure.Redis;
using Loja.Infrastructure.Storage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loja
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(c =>
                c.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None);

            services
                .AddAuthentication(AzureADDefaults.AuthenticationScheme)
                .AddAzureAD(c => Configuration.Bind("AzureAd", c));

            services.AddSingleton<IRedisCache, RedisCache>();
            services.AddScoped<ICarrinhoService, CarrinhoService>();
            services.AddScoped<IProdutoServices, ProdutoServices>();
            services.AddScoped<IAzureStorage, AzureStorage>();

            Mapper.Initialize(c => c.AddProfile<ProdutoProfile>());

            services.AddAutoMapper();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
