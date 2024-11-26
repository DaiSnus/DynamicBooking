namespace DynamicBooking.Doomain;

public class Registration
{
    public int Id { get; set; }

    public TimeSlot TimeSlot { get; set; }

    public int TimeSlotId {  get; set; }

    public Guid ParticipantId { get; set; }

    public User Participant { get; set; }
}
