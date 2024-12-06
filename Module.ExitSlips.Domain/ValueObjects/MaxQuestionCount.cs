namespace Module.ExitSlip.Domain.ValueObjects;

public record MaxQuestionCount
{
    public MaxQuestionCount(int value)
    {
        Validate(value);
        Value = value;
    }

    public int Value { get; set; }

    private void Validate(int value)
    {
        if (value <= 0)
            throw new ArgumentException("The number of questions in an exitslip can't be 0 or negative.");
        if (value > 10)
            throw new ArgumentException("The number of questions in an exitslip can't exceed 10.");
    }

    public static implicit operator MaxQuestionCount(int value)
    {
        return new MaxQuestionCount(value);
    }

    public static implicit operator int(MaxQuestionCount value)
    {
        return value.Value;
    }
}