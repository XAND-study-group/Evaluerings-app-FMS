using Module.ExitSlip.Domain.Entities;

namespace Module.ExitSlip.Domain.Test.Fakes;

public class FakeQuestion : Question
{
    public FakeQuestion()
    {
    }

    public FakeQuestion(string text)
    {
        Text = text;
    }

    public void SetText(string text)
    {
        Text = text;
    }
}