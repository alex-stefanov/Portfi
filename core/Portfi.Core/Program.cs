using DotNetEnv;
using Supabase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Portfi.Core.Extensions;
using DATA = Portfi.Data;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment;

builder.Configuration
    .AddEnvironmentSpecificJsonFiles(environment, out string connectionString);

var supabaseUrl = Environment.GetEnvironmentVariable("Supabase_Url");
var supabaseKey = Environment.GetEnvironmentVariable("Supabase_ApiKey");

if (string.IsNullOrEmpty(supabaseUrl)
    || string.IsNullOrEmpty(supabaseKey))
{
    throw new ArgumentNullException("Supabase credentials are missing!");
}

var options = new SupabaseOptions
{
    AutoConnectRealtime = true
};

builder.Services
    .AddSingleton(provider => new Client(supabaseUrl, supabaseKey, options));

builder.Services
    .AddDbContext<DATA.PortfiDbContext>(options =>
    {
        options.UseSqlServer(connectionString);
    });

builder.Services
    .RegisterRepositories(typeof(DATA.PortfiDbContext).Assembly)
    .RegisterUserDefinedServices(typeof(Portfi.Infrastructure.Services.Interfaces.IPortfolioService).Assembly);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://your-tenant-name.auth0.com/";

        options.Audience = "https://portfio.com/";
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();