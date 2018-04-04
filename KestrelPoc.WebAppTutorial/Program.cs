using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace KestrelPoc.WebAppTutorial
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildIISReverseProxyWebHost(args).Run();
        }

        public static IWebHost BuildDefaultWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel()
                .Build();

        public static IWebHost BuildIISReverseProxyWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();
    }
}
