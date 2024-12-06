using System.ComponentModel.DataAnnotations;

namespace Evaluation.Web.ViewModels;

public class FeedbackViewModel
{
    [Required] public string Title { get; set; }

    [Required] public string Problem { get; set; }

    [Required] public string Solution { get; set; }

    [Required] public Guid RoomId { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;
}