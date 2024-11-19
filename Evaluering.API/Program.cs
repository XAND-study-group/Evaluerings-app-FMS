using Microsoft.AspNetCore.Mvc;
using Module.Feedback.Domain.DomainServices;
using Module.Feedback.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFeedbackModule(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("TestGemini/",
    async ([FromBody] ContentDto content, [FromServices] IValidationServiceProxy validationServiceProxy) =>
    await validationServiceProxy.IsAcceptableContentAsync(content.Title, content.Problem, content.Solution));

app.Run();

record ContentDto(string Title, string Problem, string Solution);