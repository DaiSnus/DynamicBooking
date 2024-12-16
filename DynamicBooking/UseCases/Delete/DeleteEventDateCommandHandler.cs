using DynamicBooking.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DynamicBooking.UseCases.Delete;

public class DeleteEventDateCommandHandler : IRequestHandler<DeleteEventDateCommand, Unit>
{
    private readonly IAppDbContext appDbContext;

    public DeleteEventDateCommandHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<Unit> Handle(DeleteEventDateCommand request, CancellationToken cancellationToken)
    {
        var eventDateId = request.eventDateId;
        var editEventId = request.editEventId;

        var eventDate = await appDbContext.EventsDate
                        .Include(ed => ed.TimeSlot)
                        .ThenInclude(ts => ts.TimeRange)
                        .FirstAsync(ed => ed.Event.EventActions.EditEventId == editEventId 
                                            && ed.Id == eventDateId);

        appDbContext.EventsDate.Remove(eventDate);

        await appDbContext.SaveChangesAsync();
        
        return Unit.Value;
    }
}
