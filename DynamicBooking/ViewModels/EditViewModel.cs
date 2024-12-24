using DynamicBooking.UseCases.GetEvent;
using Microsoft.AspNetCore.Mvc;

namespace DynamicBooking.ViewModels;

[RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
public class EditViewModel
{
    public EventDto Event { get; set; }

    public IEnumerable<EventDateDto> AddedEventDates { get; init; }

    public IEnumerable<EventDateDto> NewEventDates { get; set; }

    public IEnumerable<EventDateDto> DeletedEventDates { get; set; }

    public IEnumerable<EventFileDto> AddedEventFiles { get; init; }

    public IFormFileCollection NewEventFiles { get; set; }

    public IEnumerable<EventFileDto> DeletedEventFiles { get; set; }

    public IEnumerable<EventFieldDto> AddedOptionalFields { get; init; }

    public IEnumerable<EventFieldDto> NewOptionalFields { get; set; }

    public IEnumerable<EventFieldDto> DeletedOptionalFields { get; set; }
}
