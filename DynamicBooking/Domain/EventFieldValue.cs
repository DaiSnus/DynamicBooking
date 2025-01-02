using DynamicBooking.Domain;

namespace DynamicBooking.Doomain;

public class EventFieldValue
{
    public Guid Id { get; set; }

    public Guid EventFieldId { get; set; }

    public EventField EventField { get; set; }

    public Guid RegistrationEventFieldValueId { get; set; }

    public RegistrationEventFieldValue RegistrationEventFieldValue { get; set; }

    public string Value { get; set; }
}
