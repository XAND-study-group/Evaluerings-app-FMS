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

var httpClientName = builder.Configuration["HttpClientName:EvaluationProxy"];
builder.Services.AddHttpClient(httpClientName,
    client => { client.BaseAddress = new Uri("http://localhost:5213"); })
    .AddHttpMessageHandler<AuthorizationHeaderHandler>();

builder.Services.AddScoped<AuthorizationHeaderHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();