using Module.ExitSlip.Domain.Entities;

namespace Module.ExitSlip.Domain.Test.Fakes;

public class FakeAnswer : Answer
{
    public FakeAnswer()
    {
        
    }

    public FakeAnswer(string text)
    {
        Text = text;
    }

    public void SetText(string? text)
    {
        Text = text!;
    }
}