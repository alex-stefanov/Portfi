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

app.UseAuthorization();

app.MapControllers();

app.Run();
