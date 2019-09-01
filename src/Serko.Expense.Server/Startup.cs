using System;
using System.Text;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Converters;
using Serko.Expense.Domain;
using Serko.Expense.Server.Configuration;
using Serko.Expense.Server.Controllers;
using Serko.Expense.Server.Extensions;
using Serko.Expense.Server.Formatters;
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
                .AddMvc(o =>
                {
                    o.InputFormatters.Insert(0, new EmailInputFormatter());
                })
                .AddFluentValidation()
                .AddJsonOptions(o =>
                {
                    o.SerializerSettings.Converters.Add(new StringEnumConverter(true));
                });

            services.AddDbContext<ExpenseContext>(o => o.UseInMemoryDatabase("Expense"));

            return services.AddCastle(application);
        }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IApplicationLifetime lifetime,
            ILoggerFactory factory)
        {
            var database = configuration.GetSection<Database>();

            factory.AddLog4Net();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.InitializeDatabase(database);
            app.UseMiddleware<TransactionMiddleware>();

            app.UseOpenApi();
            app.UseReDoc();
            app.UseMvcWithDefaultRoute();

            lifetime.ApplicationStopping.Register(() => application.Dispose());
        }
    }
}
