using SharedKernel.Models;
using SharedKernel.ValueObjects;

namespace Module.ExitSlip.Domain.Entities;

public class Question : Entity
{
    public Text Text { get; protected set; }
    private readonly List<Answer> _answers = [];
    public IReadOnlyCollection<Answer> Answers => _answers;
    
    protected Question()
    {
    }

    private Question(string text)
    {
        Text = text;
    }



    #region Methods
    
    internal static Question Create(string text) => new(text);

    #region AnswerHandling

    internal Answer AddAnswer(string text, Guid userId)
    {
        var answer = Answer.Create(text, userId);
        _answers.Add(answer);
        return answer;
    }

    internal Answer UpdateAnswer(Guid answerId, string newText)
    {
        var answer = GetAnswerById(answerId);
        answer.UpdateAnswer(newText);
        return answer;
    }

    internal void DeleteAnswer(Guid answerId)
    {
        var answer = GetAnswerById(answerId);
        _answers.Remove(answer);
    }

    #endregion

    #region QuestionHandling

    internal void UpdateQuestion(string newText)
    {
        Text = newText;
        _answers.Clear();
    }

    internal void DeleteQuestion()
    {
        _answers.Clear();
    }

    #endregion

    #region HelperMethods

    internal Answer GetAnswerById(Guid answerId) 
        => _answers.FirstOrDefault(a => a.Id == answerId)
           ?? throw new ArgumentException("Svar ikke fundet.");

    #endregion

    #endregion
}