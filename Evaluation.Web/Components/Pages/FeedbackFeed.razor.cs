using System.Security.Claims;
using Evaluation.Web.Services.Interfaces;
using Evaluation.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using SharedKernel.Dto.Features.Evaluering.Feedback.Command;
using SharedKernel.Dto.Features.Evaluering.Room.Query;
using SharedKernel.Dto.Features.School.Authentication.Command;
using SharedKernel.Models;

namespace Evaluation.Web.Components.Pages;

public partial class FeedbackFeed : ComponentBase
{
    [CascadingParameter] public HttpContext? HttpContext { get; set; }
    private bool ShowModal { get; set; }
    private bool ShowToast { get; set; }
    private bool StartLoading { get; set; }
    private FeedbackViewModel FeedbackViewModel { get; set; } = new();
    private IEnumerable<GetSimpleRoomResponse> Rooms { get; set; } = [];
    [Inject] public IEvaluationProxy EvaluationProxy { get; set; }
    [Inject] public ISchoolApiProxy SchoolApiProxy { get; set; }

    private async Task CreateFeedback()
    {
        await Authenticate();
        
        StartLoading = true;
        var userId = Guid.NewGuid();
        if (FeedbackViewModel.RoomId == Guid.Empty)
            return;
        var createFeedbackRequest = new CreateFeedbackRequest(userId,
            FeedbackViewModel.Title,
            FeedbackViewModel.Problem,
            FeedbackViewModel.Solution,
            FeedbackViewModel.RoomId);

        var response = await EvaluationProxy.CreateFeedbackAsync(createFeedbackRequest);
        if (response.Status == ResultStatus.Created)
        {
            CloseModal();
            OpenToast();
        }
        else
        {
            FeedbackViewModel.ErrorMessage = response.Message;
        }
        StartLoading = false;
    }

    private void OpenModal()
    {
        FeedbackViewModel = new FeedbackViewModel();
        ShowModal = true;
    }

    private void CloseModal()
        => ShowModal = false;

    private void HideToast()
        => ShowToast = false;

    private void OpenToast()
        => ShowToast = true;
    
    protected override async Task OnInitializedAsync()
    {
        Rooms = await EvaluationProxy.GetAllRoomsAsync();
    }
    
    private async Task Authenticate()
    {
        var account = new AuthenticateAccountLoginRequest("Timothy14@yahoo.com", "Password123.");
        var user = await SchoolApiProxy.AuthenticateAsync(account);

        var claims = new List<Claim>
        {
            new Claim("JWT", user.SuccessResult.AccessToken),
            new Claim("RefreshToken", user.SuccessResult.RefreshToken)
        };
        
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(principal);
    }
}