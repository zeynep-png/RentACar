using RentACar.Business.DataProtection;
using RentACar.Business.Operations.Setting;
using RentACar.Business.Operations.User;
using RentACar.Data.Context;
using RentACar.Data.Entities;
using RentACar.Data.Repositories;
using RentACar.Data.UnifOfWork;
using RentACar.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RentACar.Business.Operations.User.Dtos;
using System.Text;
using RentACar.Business.Operations.Feature;
using RentACar.Business.Operations.Car;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Register the controllers

// Configure Swagger/OpenAPI for documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "Bearer", // Authentication scheme
        BearerFormat = "Jwt", // Format of the token
        Name = "Jwt Authentication", // Name for the security definition
        In = ParameterLocation.Header, // Location of the token
        Type = SecuritySchemeType.Http, // Type of security
        Description = "Put **_ONLY_** your JWT Bearer Token on Textbox below", // Description for users

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme, // Reference ID
            Type = ReferenceType.SecurityScheme // Reference type
        }
    };
    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {jwtSecurityScheme, Array.Empty<string>() } // Require the JWT security scheme
    });
});

// Add Data Protection service
builder.Services.AddScoped<IDataProtection, DataProtection>();

// Set the directory for key storage
var keysDirectory = new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "App_Data", "Keys"));
builder.Services.AddDataProtection()
    .SetApplicationName("RentACar") // Application name for data protection
    .PersistKeysToFileSystem(keysDirectory); // Persist keys to file system

// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // Validate the token issuer
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // Valid issuer from configuration

            ValidateAudience = true, // Validate the audience
            ValidAudience = builder.Configuration["Jwt:Audience"], // Valid audience from configuration

            ValidateLifetime = true, // Validate the token lifetime

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)) // Signing key for validation
        };
    });

// Get the connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("default");

// Configure the DbContext for SQL Server
builder.Services.AddDbContext<RentACarDbContext>(options => options.UseSqlServer(connectionString));

// Register generic repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Register Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<ICarService, CarManager>();


builder.Services.AddScoped<IFeatureService, FeatureManager>();


builder.Services.AddScoped<ISettingService, SettingManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger in development
    app.UseSwaggerUI(); // Enable Swagger UI
}

app.UseMaintenanceMode(); // Use the maintenance mode middleware

app.UseHttpsRedirection(); // Redirect HTTP to HTTPS

app.UseAuthentication(); // Enable authentication

app.UseAuthorization(); // Enable authorization

app.MapControllers(); // Map the controllers

app.UseMiddleware<ExceptionHandlingMiddleware>();


app.Run(); // Run the application
