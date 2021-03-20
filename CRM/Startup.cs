using CRM.Common;
using CRM.Handlers;
using CRM.Helpers;
using CRM.Models;
using CRM.Rpc;
using CRM.Services;
using Hangfire;
using Hangfire.MySql.Core;
using Hangfire.SqlServer;
using Hangfire.Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using Newtonsoft.Json;
using OfficeOpenXml;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Thinktecture;
using Z.EntityFramework.Extensions;

namespace CRM
{
    public class Startup
    {
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();

            Configuration = builder.Build();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            LicenseManager.AddLicense("2456;100-FPT", "3f0586d1-0216-5005-8b7a-9080b0bedb5e");
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            _ = DataEntity.ErrorResource;
            services.AddControllers().AddNewtonsoftJson(
                options =>
                {
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffK";
                });
            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddSingleton<IPooledObjectPolicy<IModel>, RabbitModelPooledObjectPolicy>();
            services.AddSingleton<IRabbitManager, RabbitManager>();
            services.AddHostedService<ConsumeRabbitMQHostedService>();
            //services.AddDbContext<DataContext>(options =>
            //{
            //    options.UseSqlServer(Configuration.GetConnectionString("DataContext"), sqlOptions =>
            //    {
            //        sqlOptions.AddTempTableSupport();
            //    });
            //    options.AddInterceptors(new HintCommandInterceptor());
            //});
            services.AddDbContext<DataContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("DataContext"), sqlOptions =>
                {
                    sqlOptions.ServerVersion(new ServerVersion(new Version(8, 0, 23), ServerType.MySql));
                    //sqlOptions.AddTempTableSupport();
                    //sqlOptions.EnableRetryOnFailure();
                });
                options.AddInterceptors(new HintCommandInterceptor());
            });
            //EntityFrameworkManager.ContextFactory = context =>
            //{
            //    var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            //    optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DataContext"));
            //    DataContext DataContext = new DataContext(optionsBuilder.Options);
            //    return DataContext;
            //};
            EntityFrameworkManager.ContextFactory = context =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
                optionsBuilder.UseMySql(Configuration.GetConnectionString("DataContext"));
                DataContext DataContext = new DataContext(optionsBuilder.Options);
                return DataContext;
            };

            //services.AddHangfire(configuration => configuration
            // .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            // .UseSimpleAssemblyNameTypeSerializer()
            // .UseRecommendedSerializerSettings()
            // .UseSqlServerStorage(Configuration.GetConnectionString("DataContext"), new SqlServerStorageOptions
            // {
            //     SlidingInvisibilityTimeout = TimeSpan.FromMinutes(2),
            //     QueuePollInterval = TimeSpan.FromSeconds(10),
            //     CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            //     UseRecommendedIsolationLevel = true,
            //     UsePageLocksOnDequeue = true,
            //     DisableGlobalLocks = true
            // }));
            services.AddHangfire(configuration => configuration
             .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
             .UseSimpleAssemblyNameTypeSerializer()
             .UseRecommendedSerializerSettings()
             .UseStorage(new MySqlStorage(Configuration.GetConnectionString("Hangfire"), new MySqlStorageOptions() { TablePrefix = "Hangfire" })));

            services.AddHangfireServer();

            services.Scan(scan => scan
             .FromAssemblyOf<IServiceScoped>()
                 .AddClasses(classes => classes.AssignableTo<IServiceScoped>())
                     .AsImplementedInterfaces()
                     .WithScopedLifetime());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        //context.Token = context.Request.Cookies["Token"];
                        //context.Token = context.Request.Headers["authorization"];
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKeyResolver = (token, secutiryToken, kid, validationParameters) =>
                    {
                        var secretKey = Configuration["Config:SecretKey"];
                        var key = Encoding.ASCII.GetBytes(secretKey);
                        SecurityKey issuerSigningKey = new SymmetricSecurityKey(key);
                        return new List<SecurityKey>() { issuerSigningKey };
                    }
                };
            });

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Permission", policy =>
                    policy.Requirements.Add(new PermissionRequirement()));
            });

            services.AddScoped<IAuthorizationHandler, SimpleHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Simple", policy =>
                    policy.Requirements.Add(new SimpleRequirement()));
            });

            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            Action onChange = () =>
            {
                InternalServices.UTILS = Configuration["InternalServices:UTILS"];
                InternalServices.ES = Configuration["InternalServices:ES"];
                //JobStorage.Current = new SqlServerStorage(Configuration.GetConnectionString("DataContext"));
                JobStorage.Current = new MySqlStorage(Configuration.GetConnectionString("Hangfire"));
                using (var connection = JobStorage.Current.GetConnection())
                {
                    foreach (var recurringJob in connection.GetRecurringJobs())
                    {
                        RecurringJob.RemoveIfExists(recurringJob.Id);
                    }
                }

                string daily = "59 16 * * *"; 
                string minute = "*/1 * * * *";
                RecurringJob.AddOrUpdate<MaintenanceService>("CleanHangfire", x => x.CleanHangfire(), daily);
                RecurringJob.AddOrUpdate<MaintenanceService>("CleanEventMessage", x => x.CleanEventMessage(), daily);
                //RecurringJob.AddOrUpdate<MaintenanceService>("ReminderActivity", x => x.ReminderActivity(), minute);
                RecurringJob.AddOrUpdate<MaintenanceService>("ReminderTicket", x => x.ReminderTicket(), minute);
                RecurringJob.AddOrUpdate<MaintenanceService>("UpdateTicketSLAStatus", x => x.UpdateTicketSLAStatus(), minute);
                RecurringJob.AddOrUpdate<MaintenanceService>("UpdateKMSStatus", x => x.UpdateKMSStatus(), minute);
            };
            onChange();
            ChangeToken.OnChange(() => Configuration.GetReloadToken(), onChange);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "rpc/crm/swagger/{documentname}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/rpc/crm/swagger/v1/swagger.json", "crm API");
                c.RoutePrefix = "rpc/crm/swagger";
            });
            app.UseDeveloperExceptionPage();
            app.UseHangfireDashboard("/rpc/crm/hangfire");
        }
    }
}
