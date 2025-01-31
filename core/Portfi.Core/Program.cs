using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Portfi.Core.Extensions;
using DATA = Portfi.Data;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment;

builder.Configuration
    .AddEnvironmentSpecificJsonFiles(environment, out string connectionString);

builder.Services
    .AddDbContext<DATA.PortfiDbContext>(options =>
    {
        options.UseSqlServer(connectionString);
    });

builder.Services
    .RegisterRepositories()
    .RegisterUserDefinedServices();

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
