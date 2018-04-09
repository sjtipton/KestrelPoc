using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillyMadison.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BillyMadison
{
    public class Startup
    {
        // modified to handle secrets
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder();

            // https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?tabs=visual-studio-code
            builder.AddUserSecrets<Startup>();

            Configuration = builder.Build();

            foreach(var item in configuration.AsEnumerable())
            {
                Configuration[item.Key] = item.Value;
            }
        }

        // source: https://github.com/aspnet/Docs/blob/master/aspnetcore/security/app-secrets/sample/UserSecrets/Startup.cs
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var dbuser = Configuration["dbuser"];
            var dbpass = Configuration["dbpass"];
            var dbname = Configuration["dbname"];

            var connection = $@"Server=localhost;Database={dbname};User Id={dbuser}; Password={dbpass}";

            services.AddDbContext<BillyMadisonContext>(options => options.UseSqlServer(connection));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
