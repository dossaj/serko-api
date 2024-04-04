using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Serko.Expense.Domain;
using Serko.Expense.Server;
using Serko.Expense.Server.Configuration;
using Serko.Expense.Server.Extensions;
using Serko.Expense.Server.Middleware;
using Serko.Expense.Server.Options;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var application = new Application(builder.Configuration);
var security = builder.Configuration.GetSection<Security>();

builder.Logging.AddLog4Net();

IdentityModelEventSource.ShowPII = true;
builder.Services
    .AddAuthorization()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = false,
            ValidateLifetime = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(security.JwtKey))
        };
    });

builder.Services
    .AddDbContext<ExpenseContext>(o => o.UseInMemoryDatabase("Expense"))
    .AddSwaggerDocument(s =>
    {
        s.Title = "Expense Api";
        s.Description = "A very descriptive description";
    })
    .AddFluentValidationAutoValidation()
    .ConfigureOptions<ConfigureFormatterOptions>()
    .AddMvcCore()
    .AddApiExplorer()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    });

builder.Services
    .AddCastle(application);

var app = builder.Build();
var database = builder.Configuration.GetSection<Database>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.InitializeDatabase(database);
app.UseMiddleware<TransactionMiddleware>();

app.UseOpenApi();
app.UseReDoc();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Lifetime.ApplicationStopping.Register(() => application.Dispose());

await app.RunAsync();
