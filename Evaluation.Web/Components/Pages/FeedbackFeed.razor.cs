using Evaluation.Web.Services.Interfaces;
using Evaluation.Web.ViewModels;
using Microsoft.AspNetCore.Components;
using SharedKernel.Dto.Features.Evaluering.Room.Query;

namespace Evaluation.Web.Components.Pages;

public partial class FeedbackFeed : ComponentBase
{
    private bool IsValidInput { get; set; }
    private FeedbackViewModel FeedbackViewModel { get; set; } = new();
    private IEnumerable<GetSimpleRoomResponse> Rooms { get; set; } = [];
    [Inject] public IEvaluationProxy EvaluationProxy { get; set; }

    private void ValidateInput()
    {
        IsValidInput = true;
    }

    private void CreateFeedback()
    {
        
    }

    protected override async Task OnInitializedAsync()
    {
        Rooms = await EvaluationProxy.GetAllRoomsAsync();
    }
}