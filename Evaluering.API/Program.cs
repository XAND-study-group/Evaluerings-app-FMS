using System.Text;
using Evaluering.API;
using Evaluering.API.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Module.ExitSlip.Extension;
using Module.Feedback;
using Module.Feedback.Domain.DomainServices.Interfaces;
using Module.Feedback.Extension;
using Module.Feedback.Infrastructure.Options;
using SharedKernel.Interfaces.Helper;

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
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGenWithAuth();


builder.Services.AddMediatRModules();
builder.Services.AddEndpoints(AssemblyReference.Assembly);
builder.Services.AddEndpoints(Module.ExitSlip.AssemblyReference.Assembly);
builder.Services.AddFeedbackModule(builder.Configuration);
builder.Services.AddExitSlipModule(builder.Configuration);
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(MediatorPipelineBehavior<,>));

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, true)
    .AddUserSecrets<EvaluationSecrets>()
    .AddEnvironmentVariables();

builder.Services.Configure<EvaluationSecrets>(builder.Configuration.GetSection(nameof(EvaluationSecrets)))
    .AddOptions();

var app = builder.Build();

app.MapEndpoints(builder.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// Enable request body buffering 
app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    await next();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("TestGemini/Feedback",
    async ([FromBody] FeedbackContentDto content, [FromServices] IValidationServiceProxy validationServiceProxy) =>
    await validationServiceProxy.IsAcceptableContentAsync(content.Title, content.Problem, content.Solution));

app.MapPost("TestGemini/Comment",
    async ([FromBody] CommentContentDto content, [FromServices] IValidationServiceProxy validationServiceProxy) =>
    await validationServiceProxy.IsAcceptableCommentAsync(content.Comment));

app.Run();

namespace Evaluering.API
{
    internal record FeedbackContentDto(string Title, string Problem, string Solution);

    internal record CommentContentDto(string Comment);
}