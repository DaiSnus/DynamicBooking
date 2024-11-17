﻿using DynamicBooking.Doomain;

namespace DynamicBooking.UseCases.GetEvent;

public class EventActionsDto
{
    public Guid RegistrationEventId { get; init; }

    public Guid ResultsId { get; init; }

    public Guid EditEventId { get; init; }
}
