using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

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
            CreateWebHostBuilder(args).Build().Run();
        }
        /// <summary>
        /// Creates a WebHostBuilder to setup the application
        /// </summary>
        /// <param name="args">Args</param>
        /// <returns>Created builder</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
