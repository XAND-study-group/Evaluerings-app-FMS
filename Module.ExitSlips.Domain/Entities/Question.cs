using SharedKernel.Models;
using SharedKernel.ValueObjects;

namespace Module.ExitSlip.Domain.Entities;

public class Question : Entity
{
    #region Properties

    public Text Text { get; protected set; }
    private readonly List<Answer> _answers = [];
    public IReadOnlyCollection<Answer> Answers => _answers;

    #endregion

    #region Constructors

    protected Question()
    {
    }

    private Question(string text)
    {
        Text = text;
    }
    private Question(string text, IEnumerable<Answer> answers)
    {
        Text = text;
        _answers = answers.ToList();
    }

    internal static Question Create(string text)
    {
        return new Question(text);
    }
   
    internal static Question CreateWithAnswer(string text, IEnumerable<Answer> answers)
    {
        return new Question(text, answers);
    }

    #endregion

    #region Methods

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
    {
        var answer = _answers.FirstOrDefault(a => a.Id == answerId)
                     ?? throw new ArgumentException("Svar ikke fundet.");

        return answer;
    }

    #endregion

    #endregion
}