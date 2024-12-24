using AutoMapper;
using DynamicBooking.Domain;
using DynamicBooking.Doomain;
using DynamicBooking.Infrastructure.Abstractions;
using DynamicBooking.Infrastructure.Implementations;
using DynamicBooking.UseCases.GetEvent;
using MediatR;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DynamicBooking.UseCases.EditForm;

public class EditFormCommandHandler : IRequestHandler<EditFormCommand, EventActionsIdDto>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;
    private readonly IFileSaver fileSaver;
    private readonly IFileDeleter fileDeleter;

    public EditFormCommandHandler(IAppDbContext appDbContext, IMapper mapper, IFileSaver fileSaver, IFileDeleter fileDeleter)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
        this.fileSaver = fileSaver;
        this.fileDeleter = fileDeleter;
    }

    public async Task<EventActionsIdDto> Handle(EditFormCommand request, CancellationToken cancellationToken)
    {
        var viewModel = request.viewModel;
        var eventDto = viewModel.Event;

        var e = await appDbContext.Events
                        .Include(e => e.EventActions)
                        .Include(e => e.Owner)
                        .Include(e => e.EventDates)
                        .ThenInclude(ed => ed.TimeSlot)
                        .Include(e => e.FormFiles)
                        .Include(e => e.OptionalFields)
                        .ThenInclude(of => of.EventFieldValues)
                        .FirstAsync(ea => ea.EventActions.EditEventId == eventDto.EventActions.EditEventId, cancellationToken);

        if (viewModel.NewEventFiles != null && viewModel.NewEventFiles.Count > 0)
        {
            var newEventFileDtos = await fileSaver.SaveFilesAndGetDoomainInstancesAsync(viewModel.NewEventFiles);
            eventDto.FormFiles = newEventFileDtos;

            var mappingEventFiles = new List<EventFile>();
            foreach (var eventFileDto in newEventFileDtos)
            {
                var eventFile = mapper.Map<EventFile>(eventFileDto);
                eventFile.Event = e;
                mappingEventFiles.Add(eventFile);
            }
            await appDbContext.EventsFiles.AddRangeAsync(mappingEventFiles);
        }

        var uploadedEventFiles = appDbContext.EventsFiles;
        if (viewModel.DeletedEventFiles != null)
        {
            fileDeleter.DeleteFile(viewModel.DeletedEventFiles);
            var deletedFilesIds = viewModel.DeletedEventFiles.Select(ef => ef.Id);
            uploadedEventFiles.RemoveRange(uploadedEventFiles.Where(ef => deletedFilesIds.Contains(ef.Id)));
        }

        if (viewModel.AddedEventFiles != null && eventDto.FormFiles != null)
        {
            eventDto.FormFiles = eventDto.FormFiles.Concat(viewModel.AddedEventFiles);
        }
        else if (viewModel.AddedEventFiles != null)
        {
            eventDto.FormFiles = viewModel.AddedEventFiles;
        }

        var uploadedEventDates = appDbContext.EventsDate;

        if (viewModel.DeletedEventDates != null)
        {
            var deletedEventDatesIds = viewModel.DeletedEventDates.Select(ed => ed.Id);
            uploadedEventDates.RemoveRange(uploadedEventDates.Where(ed => deletedEventDatesIds.Contains(ed.Id)));
        }

        var eventDateWithTimeRange = uploadedEventDates.Include(ed => ed.TimeSlot)
                          .ThenInclude(ts => ts.TimeRange);

        if (viewModel.AddedEventDates != null)
        {
            if (eventDto.EventDates == null)
            {
                eventDto.EventDates = viewModel.AddedEventDates;
            }
            foreach (var addedEventDate in viewModel.AddedEventDates)
            {
                var uploadedEventDate = eventDateWithTimeRange.First(ed => ed.Id == addedEventDate.Id);
                uploadedEventDate.Date = addedEventDate.Date;
                uploadedEventDate.TimeSlot.TimeRange.StartTime = addedEventDate.TimeSlot.TimeRange.StartTime;
                uploadedEventDate.TimeSlot.TimeRange.EndTime = addedEventDate.TimeSlot.TimeRange.EndTime;
                uploadedEventDate.TimeSlot.AvailableSeats = addedEventDate.TimeSlot.AvailableSeats;
            }
        }
        if (viewModel.NewEventDates != null)
        {
            eventDto.EventDates = eventDto.EventDates.Concat(viewModel.NewEventDates);

            var mappingEventDates = new List<EventDate>();
            foreach (var eventDateDto in viewModel.NewEventDates)
            {
                var eventDate = mapper.Map<EventDate>(eventDateDto);
                eventDate.Event = e;
                mappingEventDates.Add(eventDate);
            }
            await appDbContext.EventsDate.AddRangeAsync(mappingEventDates);
        }

        var uploadedOptionalFields = appDbContext.EventsFields;

        if (viewModel.DeletedOptionalFields != null)
        {
            var deletedOptionalFieldsIds = viewModel.DeletedOptionalFields.Select(of => of.Id);
            uploadedOptionalFields.RemoveRange(uploadedOptionalFields.Where(of => deletedOptionalFieldsIds.Contains(of.Id)));
        }

        if (viewModel.AddedOptionalFields != null)
        {
            if (eventDto.OptionalFields == null || eventDto.OptionalFields.Count() == 0)
            {
                eventDto.OptionalFields = viewModel.AddedOptionalFields;
            }
            foreach (var addedOptionalField in viewModel.AddedOptionalFields)
            {
                var uploadedEventField = uploadedOptionalFields.First(of => of.Id == addedOptionalField.Id);
                uploadedEventField.Title = addedOptionalField.Title;
                uploadedEventField.Type = addedOptionalField.Type;
            }
        }
        if (viewModel.NewOptionalFields != null)
        {
            if (eventDto.OptionalFields == null)
            {
                eventDto.OptionalFields = viewModel.NewOptionalFields;
            }
            else
            {
                eventDto.OptionalFields = eventDto.OptionalFields.Concat(viewModel.NewOptionalFields);
            }

            var mappingOptionalFields = new List<EventField>();
            foreach (var eventFieldDto in viewModel.NewOptionalFields)
            {
                var eventField = mapper.Map<EventField>(eventFieldDto);
                eventField.Event = e;
                mappingOptionalFields.Add(eventField);
            }
            await appDbContext.EventsFields.AddRangeAsync(mappingOptionalFields);
        }
        
        e.Owner.Surname = eventDto.Owner.Surname;
        e.Owner.Name = eventDto.Owner.Name;
        e.Owner.Email = eventDto.Owner.Email;
        e.Owner.PhoneNumber = eventDto.Owner.PhoneNumber;

        await appDbContext.SaveChangesAsync(cancellationToken);

        return eventDto.EventActions;
    }
}
