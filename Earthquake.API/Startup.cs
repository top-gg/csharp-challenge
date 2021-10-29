using System;
using System.IO;
using System.Reflection;
using Earthquake.API.AutoMappingProfiles;
using Earthquake.Data.CSV;
using Earthquake.Data.USGS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Earthquake.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Earthquake.API", Version = "v1"});
            });

            services.AddSingleton<IUsgsHttpClient>(new UsgsHttpClient(Configuration["USGSApiBaseAddress"]));
            services.AddSingleton<IUsgsDataContext, UsgsDataContext>();

            var csvFileDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var csvFullFileName = Path.Combine(csvFileDirectory ?? throw new InvalidOperationException(), Configuration["EarthquakeDataCSVFileName"]);
            services.AddSingleton<ICsvParser>(new CsvParser(csvFullFileName));
            services.AddSingleton<ICsvDataContext, CsvDataContext>();

            services.AddAutoMapper(typeof(EarthquakeMappingProfile));

            if (HostingEnvironment.IsDevelopment())
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = Configuration["RedisConnectionString"];
                    options.InstanceName = "Earthquake";
                });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Earthquake.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}