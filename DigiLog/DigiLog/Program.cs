using DigiLog;
using DigiLog.Data;
using DigiLog.Models.ResponseModels;
using DigiLog.Services.Abstraction;
using DigiLog.Services.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;



//using DigiLog.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();


var builder = WebApplication.CreateBuilder(args);

// Configure AppSettings
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Configure JWT authentication
var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
appSettings.GenerateRandomKey(); // Ensure the key is generated dynamically
var key = Encoding.ASCII.GetBytes(appSettings.Secret);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            // Other options...
        };
    });

// Add services to the container.

string? connectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<LogDbContext>(o => o.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 2, 0))));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithMethods("OPTIONS");
    });
});

builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IVisitorService, VisitorService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseMiddleware<ApiExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
