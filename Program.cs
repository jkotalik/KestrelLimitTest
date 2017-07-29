using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace KestrelLimitTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    // options.Limits.MaxConcurrentConnections = 10;
                    // options.Limits.MaxConcurrentUpgradedConnections = 10;

                    // Test Default, below default, above default
                    //options.Limits.MaxRequestBodySize = 3000000; // 2.s86 MB
                    options.Limits.MinRequestBodyDataRate = new MinDataRate(10000, gracePeriod: TimeSpan.FromSeconds(2));
                    //options.Limits.MinRequestBodyDataRate = null;
                })
                .Build();
    }
}
