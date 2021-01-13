using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Friday
{
    /// <summary>
    /// Starts the API
    /// </summary>
    public class Program
    {
        /// <summary>
        /// You know what this is
        /// </summary>
        /// <param name="args">Args</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        /// <summary>
        /// Create HostBuilder and startup
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
