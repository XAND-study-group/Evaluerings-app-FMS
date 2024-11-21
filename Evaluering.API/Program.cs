using Evaluering.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Module.Feedback.Domain.DomainServices.Interfaces;
using Module.Feedback.Extension;
using Module.Feedback.Infrastructure.Options;
using School.API.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFeedbackModule(builder.Configuration);
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(MediatorPipelineBehavior<,>));

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<EvaluationSecrets>()
    .AddEnvironmentVariables();

builder.Services.Configure<EvaluationSecrets>(builder.Configuration.GetSection(nameof(EvaluationSecrets)))
    .AddOptions();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("TestGemini/Feedback",
    async ([FromBody] FeedbackContentDto content, [FromServices] IValidationServiceProxy validationServiceProxy) =>
    await validationServiceProxy.IsAcceptableContentAsync(content.Title, content.Problem, content.Solution));

app.MapPost("TestGemini/Comment",
    async ([FromBody] CommentContentDto content, [FromServices] IValidationServiceProxy validationServiceProxy) =>
    await validationServiceProxy.IsAcceptableCommentAsync(content.Comment));

app.Run();

namespace Evaluering.API
{
    record FeedbackContentDto(string Title, string Problem, string Solution);
    record CommentContentDto(string Comment);
}