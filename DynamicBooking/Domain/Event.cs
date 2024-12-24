using DynamicBooking.Domain;

namespace DynamicBooking.Doomain;

public class Event
{
    public Guid Id { get; set; }

    public EventActionsId EventActions {  get; set; }

    public User Owner { get; set; }

    public string Title {  get; set; }

    public string Description { get; set; }

    public IEnumerable<EventFile>? FormFiles { get; set; }

    public IEnumerable<EventDate> EventDates { get; set; }

    public IEnumerable<EventField> OptionalFields { get; set; }
}
