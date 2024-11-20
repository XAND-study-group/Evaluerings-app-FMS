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
}