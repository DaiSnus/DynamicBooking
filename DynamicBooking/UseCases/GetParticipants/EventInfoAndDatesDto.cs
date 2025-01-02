using DynamicBooking.Doomain;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DynamicBooking.UseCases.GetParticipants;

public class EventInfoAndDatesDto
{
    public string Title {  get; set; }

    public IEnumerable<EventDate> EventDates { get; set; }
}