namespace SharedKernel.Dto.Features.Evaluering.Proxy;

public record GeminiResponse(
    bool Valid,
    string Reason);