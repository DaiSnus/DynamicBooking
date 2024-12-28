using DynamicBooking.Doomain;

namespace DynamicBooking.UseCases.GetEvent;

public class EventFieldValueDto
{
    public Guid EventFieldId { get; set; }

    public string Value { get; set; }
}
