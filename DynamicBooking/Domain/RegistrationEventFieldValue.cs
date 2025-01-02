using DynamicBooking.Doomain;

namespace DynamicBooking.Domain;

public class RegistrationEventFieldValue
{
    public Guid Id { get; set; }

    public IEnumerable<EventFieldValue> EventFieldValues { get; set; }

    public IEnumerable<Registration> Registrations { get; set; }
}
