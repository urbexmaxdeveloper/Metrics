using AutoMapper;
using MetricsAgent.Mappings;
using MetricsAgent.DataAccess;
using MetricsAgent.DataAccess.DataAdapters;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NLog.Web;
using System.Data.SQLite;

namespace MetricsAgent
{
    public class Program
    {        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region ConfigureOptions

            builder.Services.Configure<DatabaseOptions>(options =>
            {
                builder.Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
            });

            #endregion

            #region Configure logging

            builder.Host.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();

            }).UseNLog(new NLogAspNetCoreOptions() { RemoveLoggerFactoryFilter = true });

            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
                logging.RequestHeaders.Add("Authorization");
                logging.RequestHeaders.Add("X-Real-IP");
                logging.RequestHeaders.Add("X-Forwarded-For");
            });

            #endregion

            #region Configure Mapping

            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new
                MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            builder.Services.AddSingleton(mapper);

            #endregion

            #region FirstStart

            //    ConfigureSqlLiteConnection();

            #endregion FirstStart

            #region RegistryServices

            builder.Services.AddControllers();
            builder.Services.AddScoped<ICpuMetricsDataAdapter, CpuMetricsDataAdapter>();
            builder.Services.AddScoped<IDotnetMetricsDataAdapter, DotnetMetricsDataAdapter>();
            builder.Services.AddScoped<IHddMetricsDataAdapter, HddMetricsDataAdapter>();
            builder.Services.AddScoped<INetworkMetricsDataAdapter, NetworkMetricsDataAdapter>();
            builder.Services.AddScoped<IRamMetricsDataAdapter, RamMetricsDataAdapter>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsAgent", Version = "v1" });

                c.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("00:00:00")
                });
            });

            #endregion

            var app = builder.Build();

            #region Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseHttpLogging();

            app.MapControllers();


            #endregion

            app.Run();
        }

        #region PrepareDB Methods

        private static void ConfigureSqlLiteConnection()
        {
            const string connectionString = "Data Source = metrics.db; Version = 3; Pooling = true; Max Pool Size = 100;";
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            PrepareSchema(connection);
        }

        private static void PrepareSchema(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
                command.ExecuteNonQuery();
                command.CommandText =
                    @"CREATE TABLE cpumetrics(id INTEGER
                    PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();
                command.CommandText =
                   @"CREATE TABLE dotnetmetrics(id INTEGER
                    PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();
                command.CommandText =
                   @"CREATE TABLE hddmetrics(id INTEGER
                    PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();
                command.CommandText =
                   @"CREATE TABLE networkmetrics(id INTEGER
                    PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();
                command.CommandText =
                  @"CREATE TABLE rammetrics(id INTEGER
                    PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();
            }
        }
    }

    #endregion


}