using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace WindowsServiceHosting
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            bool isService = true;
            if (Debugger.IsAttached || args.Contains("--console"))
            {
                isService = false;
            }

            var pathToContentRoot = Directory.GetCurrentDirectory();
            if (isService)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                pathToContentRoot = Path.GetDirectoryName(pathToExe);
            }

            //var host = new WebHostBuilder()
            //.UseKestrel()
            //.UseContentRoot(pathToContentRoot)
            //.UseIISIntegration()
            //.UseStartup<Startup>()
            //.UseApplicationInsights()
            //.Build();

            //if (isService)
            //{
            //    host.RunAsCustomService();
            //}
            //else
            //{
            //    host.Run();
            //}
        }
    }
}
