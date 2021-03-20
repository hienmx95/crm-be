using CRM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CRM
{
    public class DbContextFactor : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json")
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

            optionsBuilder.UseMySql(config.GetConnectionString("DataContext"), x => x
                    .ServerVersion(new ServerVersion(new Version(8, 0, 23), ServerType.MySql))
                    .MigrationsAssembly(typeof(DataContext).GetTypeInfo().Assembly.GetName().Name)
                    .SchemaBehavior(MySqlSchemaBehavior.Ignore)
                    );

            return new DataContext(optionsBuilder.Options);
        }
    }
}
