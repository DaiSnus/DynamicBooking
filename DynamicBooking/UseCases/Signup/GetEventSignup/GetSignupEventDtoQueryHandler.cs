using AutoMapper;
using DynamicBooking.Doomain;
using DynamicBooking.Infrastructure.Abstractions;
using DynamicBooking.UseCases.GetEvent;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DynamicBooking.UseCases.Signup.GetEventSignup;

public class GetSignupEventDtoQueryHandler : IRequestHandler<GetSignupEventDtoQuery, EventDto>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;
    
    public GetSignupEventDtoQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    public async Task<EventDto> Handle(GetSignupEventDtoQuery request, CancellationToken cancellationToken)
    {
        var registrationEventId = request.registrationEventId;

        var e = await appDbContext.Events
                       .Include(e => e.EventActions)
                       .Include(e => e.Owner)
                       .Include(e => e.EventDates)
                       .ThenInclude(ed => ed.TimeSlot)
                       .ThenInclude(ts => ts.Registrations)
                       .ThenInclude(r => r.Participant)
                       .Include(e => e.EventDates)
                       .ThenInclude(ed => ed.TimeSlot)
                       .ThenInclude(ts => ts.TimeRange)
                       .Include(e => e.FormFiles)
                       .Include(e => e.OptionalFields)
                       .ThenInclude(of => of.EventFieldValues)
                       .FirstAsync(e => e.EventActions.RegistrationEventId == registrationEventId);

        var eventDto = mapper.Map<EventDto>(e);

        return eventDto;
    }
}