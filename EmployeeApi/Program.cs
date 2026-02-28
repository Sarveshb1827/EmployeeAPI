
using EmployeeApi.Repositories;
using Microsoft.AspNetCore.Connections;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});
builder.WebHost.UseUrls("http://0.0.0.0:8080");

var app = builder.Build();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
