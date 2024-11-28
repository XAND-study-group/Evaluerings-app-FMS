using Evaluation.Web.Services.Interfaces;
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
          
        /*HttpResponseMessage response = await _httpClient.GetAsync($"/Room/AllRooms");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();*/
        return response.SuccessResult;
    }
}