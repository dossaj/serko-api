using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Converters;
using Serko.Expense.Server.Configuration;
using Serko.Expense.Server.Extensions;
using Serko.Expense.Server.Middleware;

namespace Serko.Expense.Server
{
    public class Startup
    {
        private readonly Application application;
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.application = new Application(configuration);
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var security = configuration.GetSection<Security>();
            
            services
                .AddAuthentication(o =>
                {
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(security.JwtKey))
                    };
                });

            services.AddSwaggerDocument(s =>
            {
                s.Title = "Expense Api";
                s.Description = "A very descriptive description";
            });

            services
                .AddMvc()
                .AddJsonOptions(o =>
                {
                    o.SerializerSettings.Converters.Add(new StringEnumConverter(true));
                });

            return services.AddCastle(application);
        }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IApplicationLifetime lifetime,
            ILoggerFactory factory)
        {
            factory.AddLog4Net();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<TransactionMiddleware>();

            app.UseOpenApi();
            app.UseReDoc();
            app.UseMvcWithDefaultRoute();

            lifetime.ApplicationStopping.Register(() => application.Dispose());
        }
    }
}
