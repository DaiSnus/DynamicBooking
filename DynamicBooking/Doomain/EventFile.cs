namespace DynamicBooking.Doomain;

public class EventFile
{
    public Guid Id { get; set; }

    public Guid EventId { get; set; }

    public Event Event { get; set; }

    public string FileName { get; set; }

    public string FilePath { get; set; }
}
