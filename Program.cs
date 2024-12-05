using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SecureApiWithJwt.Data;
using SecureApiWithJwt.Services;
using System.Text;
using DotNetEnv;
using System.IdentityModel.Tokens.Jwt;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add JwtService
builder.Services.AddScoped<JwtService>();

// Configure Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Environment.GetEnvironmentVariable("JWT_KEY");
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException("JWT_KEY", "JWT_KEY cannot be null or empty");
        }

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
            ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            NameClaimType = JwtRegisteredClaimNames.Sub // Map 'sub' claim to User.Identity.Name
        };
    });


Console.WriteLine($"JWT_ISSUER: {builder.Configuration["JWT_ISSUER"]}");
Console.WriteLine($"JWT_AUDIENCE: {builder.Configuration["JWT_AUDIENCE"]}");
Console.WriteLine($"JWT_KEY: {builder.Configuration["JWT_KEY"]}");

// Add Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("InstructorPolicy", policy => policy.RequireRole("Admin", "Instructor")); // Include both Admin and Instructor roles
    options.AddPolicy("StudentPolicy", policy => policy.RequireRole("Admin", "Student")); // Include both Admin and Student roles
});


var app = builder.Build();

// Enable authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
