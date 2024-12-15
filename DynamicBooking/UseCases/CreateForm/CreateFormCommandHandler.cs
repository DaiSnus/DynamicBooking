using AutoMapper;
using DynamicBooking.Domain;
using DynamicBooking.Doomain;
using DynamicBooking.Infrastructure.Abstractions;
using DynamicBooking.UseCases.GetEvent;
using MediatR;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DynamicBooking.UseCases.CreateForm;

public class CreateFormCommandHandler : IRequestHandler<CreateFormCommand, EventActionsIdDto>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;
    private readonly IFileSaver fileSaver;


    public CreateFormCommandHandler(IAppDbContext appDbContext, IMapper mapper, IFileSaver fileSaver)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
        this.fileSaver = fileSaver;
    }

    public async Task<EventActionsIdDto> Handle(CreateFormCommand request, CancellationToken cancellationToken)
    {
        var viewModel = request.viewModel;
        var eventDto = viewModel.Event;

        eventDto.EventActions = new EventActionsIdDto
        {
            EditEventId = Guid.NewGuid(),
            RegistrationEventId = Guid.NewGuid(),
            ResultsId = Guid.NewGuid()
        };

        if (viewModel.EventFiles != null && viewModel.EventFiles.Count > 0)
        {
            eventDto.FormFiles = await fileSaver.SaveFilesAndGetDoomainInstances(viewModel.EventFiles);
        }
        eventDto.EventDates = viewModel.EventDates;
        eventDto.OptionalFields= viewModel.OptionalFields;

        var e = mapper.Map<Event>(eventDto);

        await appDbContext.Events.AddAsync(e);

        await appDbContext.SaveChangesAsync();

        return eventDto.EventActions;
    }
}