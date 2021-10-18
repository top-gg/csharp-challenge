using Earthquake.API.AutoMappingProfiles;
using Earthquake.Data.CSV;
using Earthquake.Data.USGS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;

namespace Earthquake.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;

                    var usgsApiBaseAddress = configuration["USGSApiBaseAddress"];
                    services.AddSingleton<IUsgsHttpClient>(new UsgsHttpClient(usgsApiBaseAddress));
                    services.AddSingleton<IUsgsDataContext, UsgsDataContext>();

                    var earthquakeDataCsvFileName = configuration["EarthquakeDataCSVFileName"];
                    var csvFileDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                    var csvFullFileName = Path.Combine(csvFileDirectory ?? throw new InvalidOperationException(), earthquakeDataCsvFileName);
                    services.AddSingleton<ICsvParser>(new CsvParser(csvFullFileName));
                    services.AddSingleton<ICsvDataContext, CsvDataContext>();

                    services.AddAutoMapper(typeof(EarthquakeMappingProfile));
                });
        }
    }
}