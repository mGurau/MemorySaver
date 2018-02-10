using Autofac;
using AutoMapper;
using MemorySaver.Configuration.AutoFacModules;
using MemorySaver.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MemorySaver.Api
{
    public partial class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddAutoMapper();
            services.AddDbContext<MemorySaverDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MemorySaver")));

            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = 5242880;
                options.ValueLengthLimit = 5242880; // Max upload size for files is 10 MB.
                options.MultipartBodyLengthLimit = 5242880;
                options.MultipartHeadersLengthLimit = 5242880;
            });

            services.AddCors(options =>
            {
                var corsBuilder = new CorsPolicyBuilder();
                corsBuilder.AllowAnyHeader();
                corsBuilder.AllowAnyMethod();
                corsBuilder.WithOrigins(Configuration["WhiteList"]);
                corsBuilder.AllowCredentials();
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });

            ConfigureAuthentication(services);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoriesModule());
            builder.RegisterModule(new ServicesModule());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("SiteCorsPolicy");

            app.UseAuthentication();

            app.UseStaticFiles();
            
            AddAuth(app);

            app.UseMvc();
        }
    }
}
