namespace DynamicBooking.Doomain;

public class Registration
{
    public Guid Id { get; set; }

    public TimeSlot TimeSlot { get; set; }

    public Guid TimeSlotId {  get; set; }

    public IEnumerable<EventFieldValue> EventFieldValues { get; set; }

    public Guid ParticipantId { get; set; }

    public User Participant { get; set; }
}
