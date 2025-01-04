using System.Net.Http.Headers;
using Evaluation.Web.Components;
using Evaluation.Web.Handler;
using Evaluation.Web.Services;
using Evaluation.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.Cookie.Name = "auth_token";
        option.LoginPath = "/login";
        option.Cookie.MaxAge = TimeSpan.FromDays(7);
        option.AccessDeniedPath = "/access-denied";
    });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IEvaluationProxy, EvaluationProxy>();

var evaluationApiUrl = builder.Configuration["Api:EvaluationUrl"] ??
                       throw new Exception("EvaluationApiUrl configuration is missing");
builder.Services.AddHttpClient<IEvaluationProxy, EvaluationProxy>(client =>
    {
        client.BaseAddress = new Uri(evaluationApiUrl);
        // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
           // "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJmN2RjNDE5NC1kNzM5LTQwYjUtNjA3OS0wOGRkMTY0YTBjYmMiLCJlbWFpbCI6IlRpbW90aHkxNEB5YWhvby5jb20iLCJuYW1lIjoiVGltb3RoeSBIZWF0aGNvdGUiLCJSb2xlIjoiVXNlciIsIlBlcm1pc3Npb24iOlsiUmVhZEZlZWRiYWNrIiwiUG9zdEZlZWRiYWNrIiwiQ29tbWVudE9uRmVlZGJhY2siLCJBbnN3ZXJFeGl0U2xpcCIsIlZvdGVPbkZlZWRiYWNrIiwiUmVhZFJvb20iXSwiQ2xhc3MiOiJhZGYxZGUyYS0yNGFmLTRmMGEtZjdmZi0wOGRkMTY0YTBjYjAiLCJhdWQiOiJFdmFsdWF0aW5nQXBwLldlYiIsImlzcyI6IlNjaG9vbC5BUEkiLCJleHAiOjE4OTM2NzYzNDIsImlhdCI6MTczNTkwOTk0MiwibmJmIjoxNzM1OTA5OTQyfQ.KfYVMkFq8ppFYIr6ouzsN_y_jVbVJ0GKlx8KeUfmwN0");
    })
    .AddHttpMessageHandler<AuthorizationHeaderHandler>();

var schoolApiUrl = builder.Configuration["Api:SchoolUrl"] ??
                   throw new Exception("SchoolApiUrl configuration is missing");
builder.Services.AddHttpClient<ISchoolApiProxy, SchoolApiProxy>(client =>
{
    client.BaseAddress = new Uri(schoolApiUrl);
    // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
       // "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJmN2RjNDE5NC1kNzM5LTQwYjUtNjA3OS0wOGRkMTY0YTBjYmMiLCJlbWFpbCI6IlRpbW90aHkxNEB5YWhvby5jb20iLCJuYW1lIjoiVGltb3RoeSBIZWF0aGNvdGUiLCJSb2xlIjoiVXNlciIsIlBlcm1pc3Npb24iOlsiUmVhZEZlZWRiYWNrIiwiUG9zdEZlZWRiYWNrIiwiQ29tbWVudE9uRmVlZGJhY2siLCJBbnN3ZXJFeGl0U2xpcCIsIlZvdGVPbkZlZWRiYWNrIiwiUmVhZFJvb20iXSwiQ2xhc3MiOiJhZGYxZGUyYS0yNGFmLTRmMGEtZjdmZi0wOGRkMTY0YTBjYjAiLCJhdWQiOiJFdmFsdWF0aW5nQXBwLldlYiIsImlzcyI6IlNjaG9vbC5BUEkiLCJleHAiOjE4OTM2NzYzNDIsImlhdCI6MTczNTkwOTk0MiwibmJmIjoxNzM1OTA5OTQyfQ.KfYVMkFq8ppFYIr6ouzsN_y_jVbVJ0GKlx8KeUfmwN0");
});
builder.Services.AddScoped<AuthorizationHeaderHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();