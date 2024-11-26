using AutoMapper;
using DynamicBooking.Doomain;
using DynamicBooking.Infrastructure.Abstractions;
using DynamicBooking.UseCases.GetEvent;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DynamicBooking.UseCases.Signup;

public class SignupCommandHandler : IRequestHandler<SignupCommand, Unit>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    public SignupCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(SignupCommand request, CancellationToken cancellationToken)
    {
        var eventSignupDto = request.eventSignupDto;

        var e = await appDbContext.Events
                        .Include(e => e.EventActions)
                        .Include(e => e.OptionalFields)
                        .ThenInclude(of => of.EventFieldValues)
                        .Include(e => e.EventActions)
                        .Include(e => e.EventDates)
                        .ThenInclude(ed => ed.TimeSlot)
                        .ThenInclude(ts => ts.Registrations)
                        .Include(e => e.FormFiles)
                        .FirstAsync(e => e.EventActions.RegistrationEventId == eventSignupDto.EventActions.RegistrationEventId);



        return Unit.Value;
    }
}
