using DynamicBooking.Infrastructure.Abstractions;
using DynamicBooking.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace DynamicBooking.UseCases.GetParticipants;

public class GetParticipantsDtoQueryHandler : IRequestHandler<GetParticipantsDtoQuery, ParticipantsDto>
{
    private readonly IAppDbContext appDbContext;

    public GetParticipantsDtoQueryHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<ParticipantsDto> Handle(GetParticipantsDtoQuery request, CancellationToken cancellationToken)
    {
        var eventDateId = request.eventDateId;

        var eventDates = await appDbContext.EventsDate.Include(ed => ed.TimeSlot)
                                                      .ThenInclude(ts => ts.Registrations)
                                                      .ThenInclude(r => r.Participant)
                                                      .Include(ed => ed.TimeSlot)
                                                      .ThenInclude(ts => ts.Registrations)
                                                      .ThenInclude(r => r.RegistrationEventFieldValue)
                                                      .Include(ed => ed.TimeSlot)
                                                      .ThenInclude(ts => ts.Registrations)
                                                      .ThenInclude(r => r.RegistrationEventFieldValue)
                                                      .ThenInclude(r => r.EventFieldValues)
                                                      .ThenInclude(efv => efv.EventField)
                                                      .FirstAsync(ed => ed.Id == eventDateId);

        var participantsDto = new ParticipantsDto{ Registrations = eventDates.TimeSlot.Registrations };

        return participantsDto;
    }
}