namespace DynamicBooking.Doomain;

public class Registration
{
    public int Id { get; set; }

    public Guid EventId { get; set; }

    public Event Event { get; set; }

    public Guid ParticipantId { get; set; }

    public User Participant { get; set; }
}
