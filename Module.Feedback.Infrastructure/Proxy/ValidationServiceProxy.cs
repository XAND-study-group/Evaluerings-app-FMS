using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Module.Feedback.Domain.DomainServices;
using Module.Feedback.Infrastructure.Proxy.ProxyModels;

namespace Module.Feedback.Infrastructure.Proxy;

public class ValidationServiceProxy(IConfiguration configuration, HttpClient httpClient) : IValidationServiceProxy
{
    async Task<bool> IValidationServiceProxy.IsAcceptableContentAsync(string content)
    {
        string prompt =             "Your role is to validate the following content: " +
        "In your validation you need to follow at least these guidelines: " +
        "Guideline One -> The content cannot include curse words " +
        "Guideline Two -> The content cannot refer to any acts of violence " +
        "Guideline Three -> The content must be constructive feedback. Meaning that it either has to describe a problem and a solution for this problem " +
        "Your answer has to be a boolean answer so either true or false. True if the content follows the guidelines, and false if the content doesn't follow the guidelines " +
        "Your answer is not allowed to include anything else than this " +
        "The Content is: " +
        content;

        return await SendRequest(prompt);
    }

    async Task<bool> IValidationServiceProxy.IsAcceptableTitleAsync(string title)
    {
        throw new NotImplementedException();
    }

    private async Task<bool> SendRequest(string prompt)
    {
        var url = configuration["GeminiApiURL"];
        var client = new HttpClient();
        
        var theContent = new StringContent($"{{\"contents\":[{{\"parts\":[{{\"text\":\"{prompt}\"}}]}}]}}", Encoding.UTF8, "application/json");
        
        var response = await client.PostAsync(url, theContent);
        response.EnsureSuccessStatusCode();

        using var jsonDocument = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
        var answer = jsonDocument.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();

        var canParse = bool.TryParse(answer, out var answerBool);

        return canParse && answerBool;
    }
}