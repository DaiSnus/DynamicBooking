using AutoMapper;
using DynamicBooking.Domain;
using DynamicBooking.Doomain;
using DynamicBooking.Infrastructure.Abstractions;
using DynamicBooking.UseCases.GetEvent;
using MediatR;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DynamicBooking.UseCases.CreateForm;

public class CreateFormCommandHandler : IRequestHandler<CreateFormCommand, EventActionsDto>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContextAccessor;

    public CreateFormCommandHandler(IAppDbContext appDbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<EventActionsDto> Handle(CreateFormCommand request, CancellationToken cancellationToken)
    {
        var eventDto = request.eventDto;

        eventDto.EventActions = new EventActionsDto
        {
            EditEventId = Guid.NewGuid(),
            RegistrationEventId = Guid.NewGuid(),
            ResultsId = Guid.NewGuid()
        };

        var e = mapper.Map<Event>(eventDto);

        await appDbContext.Events.AddAsync(e);

        await appDbContext.SaveChangesAsync();

        return eventDto.EventActions;
    }
}
