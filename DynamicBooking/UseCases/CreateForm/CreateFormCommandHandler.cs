using AutoMapper;
using DynamicBooking.Doomain;
using DynamicBooking.Infrastructure.Abstractions;
using DynamicBooking.UseCases.GetEvent;
using MediatR;
using System.Runtime.InteropServices;

namespace DynamicBooking.UseCases.CreateForm;

public class CreateFormCommandHandler : IRequestHandler<CreateFormCommand, EventUrls>
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

    public async Task<EventUrls> Handle(CreateFormCommand request, CancellationToken cancellationToken)
    {
        var eventDto = request.eventDto;

        eventDto.EventActions = new EventActionsDto
        {
            EditEventId = Guid.NewGuid(),
            RegistrationEventId = Guid.NewGuid(),
            ResultsUrl = Guid.NewGuid()
        };

        var eventUrls = GetEventUrls(eventDto);

        var e = mapper.Map<Event>(eventDto);

        await appDbContext.Events.AddAsync(e);

        await appDbContext.SaveChangesAsync();

        return eventUrls;
    }

    public EventUrls GetEventUrls(EventDto eventDto)
    {
        if (httpContextAccessor.HttpContext == null)
        {
            throw new InvalidOperationException("Cannot get HTTP context.");
        }

        var editUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}" +
            $"://{httpContextAccessor.HttpContext.Request.Host}/edit/{eventDto.EventActions.EditEventId}";

        var registerUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}" +
            $"://{httpContextAccessor.HttpContext.Request.Host}/register/{eventDto.EventActions.RegistrationEventId}";

        var resultsUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}" +
            $"://{httpContextAccessor.HttpContext.Request.Host}/results/{eventDto.EventActions.ResultsUrl}";

        return new EventUrls
        {
            EditEventUrl = editUrl,
            RegistrationEventUrl = registerUrl,
            ResultsUrl = resultsUrl
        };
    }
}
