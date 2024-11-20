using Microsoft.AspNetCore.RateLimiting;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Module.Semester.Extensions;
using Module.User.Extensions;
using School.API;
using School.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorizationWithPolicies();

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddMemoryCache();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuth();

builder.Services.AddMediatRModules();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add endpoints
builder.Services
    .AddEndpoints(Module.User.AssemblyReference.Assembly)
    .AddEndpoints(Module.Semester.AssemblyReference.Assembly);

builder.Services
    .AddUserModule(builder.Configuration)
    .AddSemesterModule(builder.Configuration);

builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: "baseLimit", options =>
    {
        options.PermitLimit = 5;
        options.Window = TimeSpan.FromMinutes(10);
    }));

var app = builder.Build();

app.UseRateLimiter();
app.MapEndpoints(builder.Configuration);

app.UseSwagger();
app.UseSwaggerUI();
    
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

namespace School.API
{
    record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}