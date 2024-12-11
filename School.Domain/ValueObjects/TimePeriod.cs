namespace School.Domain.ValueObjects;

public class TimePeriod
{
    public TimePeriod(TimeOnly from, TimeOnly to)
    {
        Validate(from, to);
        From = from;
        To = to;
        Duration = to - from;
    }

    public TimeOnly From { get; init; }
    public TimeOnly To { get; init; }
    public TimeSpan Duration { get; protected set; }

    private void Validate(TimeOnly from, TimeOnly to)
    {
        if (from > to)
            throw new ArgumentException("Start time cannot be greater than end time.");

        if (from == to)
            throw new ArgumentException("Start time cannot be equal to end time.");
    }
}