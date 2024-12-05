using Module.Feedback.Domain.DomainServices.Interfaces;
using SharedKernel.Enums.Features.Vote;

namespace Module.Feedback.Domain.Test.Fakes;

public class FakeFeedback : Feedback
{
    public FakeFeedback()
    {
        
    }
    
    public void SetTitle(string title)
    => Title = title;
    
    public void SetProblem(string problem)
    => Problem = problem;
    
    public void SetSolution(string solution)
    => Solution = solution;

    public new void AddComment(Guid userId, string commentText, IValidationServiceProxy validationServiceProxy)
    => base.AddComment(userId, commentText, validationServiceProxy);

    public new void AddVote(Guid userId, VoteScale voteScale)
    => base.AddVote(userId, voteScale);
}