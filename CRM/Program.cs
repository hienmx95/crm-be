using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CRM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://localhost:8881");// "http://103.153.69.10:8881" Configuration.GetConnectionString("Hangfire")
                });
    }
}
