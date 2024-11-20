﻿using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Module.Feedback.Domain.DomainServices;
using SharedKernel.Dto.Features.Evaluering.Proxy;

namespace Module.Feedback.Infrastructure.Proxy;

public class ValidationServiceProxy(IConfiguration configuration, HttpClient httpClient) : IValidationServiceProxy
{
    async Task<GeminiResponse> IValidationServiceProxy.IsAcceptableContentAsync(string problem)
    {
        var prompt =
            "Din opgave er at validere følgende indhold: " +
            "I din validering skal du følge mindst disse retningslinjer: " +
            "Retningslinje Et -> Indholdet må ikke indeholde bandeord " +
            "Retningslinje To -> Indholdet må ikke henvise eller opfordre til nogen voldshandlinger " +
            "Retningslinje Tre -> Indholdet skal være konstruktiv feedback. Det betyder, at det skal beskrive et problem og en løsning på dette problem. Det skal være lovligt at skrive man ingen løsning har på problemet. " +
            "Retningslinje Fire -> Hvis indholdet er rettet imod en person, må det ikke være upassende / uhøfligt " +
            "Retningslinje Fem -> Indholdet må ikke være politisk eller religiøst " +
            "Retningslinje Seks -> Indholdet må ikke indeholde tilbud om at købe eller sælge noget, herunder billetter, varer eller tjenester. " + 
            "Retningslinje Syv -> Indholdet skal være relevant i forhold til den feedback der gives. " + 
            "Retningslinje Otte -> Indholdet skal være specifikt og undgå generaliseringer. " +
            "Retningslinje Ni -> Indholdet skal være relevant for skolearbejde og undervisning. Feedback må ikke handle om personlige emner, byttehandler eller andre irrelevante ting. " +
            "Dit svar skal være i JSON-format som følger: { Valid: 'boolean', Reason: 'string' } Formatter ikke svaret i et kodeblok." +
            "'Valid' skal være sandt, hvis indholdet følger retningslinjerne, og falsk, hvis indholdet ikke følger retningslinjerne " +
            "'Reason' skal specificere, hvorfor 'Valid' er sat til falsk. Dette skal gøres i maksimalt 10 ord. Hvis 'Valid' er sandt, skal 'Reason' være tom " +
            "Indholdet er: " +
            problem
            ;

        return await SendRequest(prompt);
    }

    async Task<GeminiResponse> IValidationServiceProxy.IsAcceptableTitleAsync(string title)
    {
        var prompt =
            "In your validation you need to follow at least these guidelines: " +
            "Guideline One -> The title cannot include curse words " +
            "Guideline Two -> The title cannot refer to or encourage any acts of violence " +
            "Your answer has to be a boolean answer so either true or false. True if the content follows the guidelines, and false if the content doesn't follow the guidelines " +
            "Your answer is not allowed to include anything else than this " +
            "The title is: " +
            title;

        return await SendRequest(prompt);
    }

    private async Task<GeminiResponse> SendRequest(string prompt)
    {
        var url = configuration["GeminiApiURL"];
        var client = new HttpClient();

        var theContent = new StringContent($"{{\"contents\":[{{\"parts\":[{{\"text\":\"{prompt}\"}}]}}]}}",
            Encoding.UTF8, "application/json");

        var response = await client.PostAsync(url, theContent);
        response.EnsureSuccessStatusCode();

        using var jsonDocument = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
        var answer = jsonDocument.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();

        return JsonSerializer.Deserialize<GeminiResponse>(answer ?? string.Empty) ?? new GeminiResponse(false, "Invalid response");
    }
}