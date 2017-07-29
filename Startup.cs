using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http.Features;
using System.IO;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;

namespace KestrelLimitTest
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.Run(async (context) =>
            {
                context.Features.Get<IHttpMinResponseDataRateFeature>().MinDataRate = new Microsoft.AspNetCore.Server.Kestrel.Core.MinDataRate(5000, TimeSpan.FromSeconds(2));
                Console.WriteLine("About to read data");

                for (var i = 0; i < 1; i++) {
                    await context.Response.WriteAsync("Hello world!Hello world!Hello world!Hello world!Hello world!Hello world!Hello world!Hello world!Hello world!", context.RequestAborted);
                }
                Console.WriteLine("Done with request");
            });
        }
    }
}
