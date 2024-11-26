using DynamicBooking.Doomain;
using DynamicBooking.UseCases.GetEvent;
using Microsoft.AspNetCore.Mvc;

namespace DynamicBooking.ViewModels;

[RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
public class FormViewModel
{
    public EventDto Event { get; set; }

    public IEnumerable<EventDateDto> EventDates { get; set; }

    public IFormFileCollection EventFiles { get; set; }

    public IEnumerable<EventFieldDto>? OptionalFields { get; init; }
}