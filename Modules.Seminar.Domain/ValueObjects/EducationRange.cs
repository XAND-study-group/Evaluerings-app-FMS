namespace Module.Semester.Domain.ValueObjects;

public record EducationRange
{
    public DateOnly Start { get; protected set; }
    public DateOnly End { get; protected set; }

    private EducationRange(DateOnly start, DateOnly end)
    {
        var now = DateOnly.FromDateTime(DateTime.Now);
        
        Validate(start, end, now);
        Start = start;
        End = end;
    }

    public static EducationRange Create(DateOnly start, DateOnly end)
        => new EducationRange(start, end);

    private void Validate(DateOnly start, DateOnly end, DateOnly now)
    {
        if (start >= end)
            throw new ArgumentException("End date has to be after the start date.");
        
        if (start <= now)
            throw new ArgumentException("Start date has to be in the future.");
    }
}