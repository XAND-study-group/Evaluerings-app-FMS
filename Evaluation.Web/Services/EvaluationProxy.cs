using Evaluation.Web.Services.Interfaces;
using SharedKernel.Dto.Features.Evaluering.Feedback.Command;
using SharedKernel.Dto.Features.Evaluering.Room.Query;
using SharedKernel.Models;

namespace Evaluation.Web.Services;

public class EvaluationProxy : IEvaluationProxy
{
    
    private readonly HttpClient _httpClient;
    public EvaluationProxy(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        var httpClientName = configuration["HttpClientName:EvaluationProxy"];
        _httpClient = httpClientFactory.CreateClient(httpClientName ?? string.Empty);
    }

    async Task<IEnumerable<GetSimpleRoomResponse>> IEvaluationProxy.GetRoomsByClassIdAsync(Guid classId)
    => await _httpClient.GetFromJsonAsync<IEnumerable<GetSimpleRoomResponse>>($"/Room/MyRooms/{classId:guid}")
    ?? throw new ArgumentException("Der kunne ikke findes nogle forums");

    async Task<IEnumerable<GetSimpleRoomResponse>> IEvaluationProxy.GetAllRoomsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<Result<IEnumerable<GetSimpleRoomResponse>>>("/Room/AllRooms")
               ?? throw new ArgumentException("Der kunne ikke findes nogle forums");

        return response.SuccessResult;
    }

    public async Task<Result<bool>> CreateFeedbackAsync(CreateFeedbackRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("/Room/CreateFeedback", request);

        var responseContent = response.Content.ReadFromJsonAsync<Result<bool>>();
        return responseContent.Result ?? Result<bool>.Create("Noget gik galt", false, ResultStatus.Error);
    }
}