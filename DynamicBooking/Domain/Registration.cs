using DynamicBooking.Domain;

namespace DynamicBooking.Doomain;

public class Registration
{
    public Guid Id { get; set; }

    public TimeSlot TimeSlot { get; set; }

    public Guid TimeSlotId {  get; set; }

    public Guid RegistrationEventFieldValueId { get; set; }

    public RegistrationEventFieldValue RegistrationEventFieldValue { get; set; }

    public Guid ParticipantId { get; set; }

    public User Participant { get; set; }
}
