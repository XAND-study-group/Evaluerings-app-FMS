using Evaluering.API;
using Evaluering.API.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Infrastructure.DbContexts;
using Module.ExitSlip.Infrastructure.Mapper;
using Module.Feedback.Domain.DomainServices.Interfaces;
using Module.Feedback.Extension;
using Module.Feedback.Infrastructure.Mapper;
using Module.Feedback.Infrastructure.Options;
using SharedKernel.Interfaces.Helper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorizationWithPolicies();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatRModules();
builder.Services.AddEndpoints(Module.Feedback.AssemblyReference.Assembly);
builder.Services.AddFeedbackModule(builder.Configuration);
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(MediatorPipelineBehavior<,>));

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
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