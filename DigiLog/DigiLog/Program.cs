using DigiLog.Data;
using DigiLog.Services.Abstraction;
using DigiLog.Services.Implementation;
using Microsoft.EntityFrameworkCore;

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IVisitorService, VisitorService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddTransient<ITagService, TagService>();
builder.Services.AddLogging();


string? connectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<LogDbContext>(o => o.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 2, 0)) ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
