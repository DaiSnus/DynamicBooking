using AutoMapper;
using DynamicBooking.Doomain;
using DynamicBooking.Infrastructure.Abstractions;
using DynamicBooking.UseCases.GetEvent;
using MediatR;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace DynamicBooking.UseCases.Signup;

public class SignupCommandHandler : IRequestHandler<SignupCommand, IEnumerable<RegistrationSuccessDto>>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;
    private readonly IFileSaver fileSaver;

    public SignupCommandHandler(IAppDbContext appDbContext, IMapper mapper, IFileSaver fileSaver)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
        this.fileSaver = fileSaver;
    }

    public async Task<IEnumerable<RegistrationSuccessDto>> Handle(SignupCommand request, CancellationToken cancellationToken)
    {
        var viewModel = request.signupViewModel;
        var eventDto = viewModel.Event;

        var registrationSuccesses = new List<RegistrationSuccessDto>();

        var e = await appDbContext.Events
                        .Include(e => e.EventActions)
                        .Include(e => e.OptionalFields)
                        .ThenInclude(of => of.EventFieldValues)
                        .Include(e => e.EventActions)
                        .Include(e => e.EventDates)
                        .ThenInclude(ed => ed.TimeSlot)
                        .ThenInclude(ts => ts.Registrations)
                        .FirstAsync(e => e.EventActions.RegistrationEventId == eventDto.EventActions.RegistrationEventId);

        var newEventFieldValue = new List<EventFieldValue>();

        var eventFieldsValues = viewModel.EventFieldsValue;
        if (eventFieldsValues != null && eventFieldsValues.Count() > 0)
        {
            if (viewModel.ParticipantFiles != null)
            {
                var newFileDtos = (List<EventFileDto>)await fileSaver.SaveFilesAndGetDoomainInstancesAsync(viewModel.ParticipantFiles, 
                                                                                                    directory: "Files\\ParticipantFiles");

                var eventFieldFiles = eventFieldsValues.Where(efv => efv.Value == null);
                var eventFieldFilesCount = eventFieldFiles.Count();
                foreach (var eventFieldValueDto in eventFieldFiles)
                {
                    var i = 0;
                    newEventFieldValue.Add(new EventFieldValue
                    {
                        EventField = e.OptionalFields.First(of => of.Id == eventFieldValueDto.EventFieldId),
                        Value = newFileDtos[i].FilePath,
                        EventFieldId = eventFieldValueDto.EventFieldId,
                    });
                    i++;
                }
            }

            foreach (var eventFieldValueDto in eventFieldsValues.Where(efv => efv.Value != null))
            {
                newEventFieldValue.Add(new EventFieldValue
                {
                    EventField = e.OptionalFields.First(of => of.Id == eventFieldValueDto.EventFieldId),
                    EventFieldId = eventFieldValueDto.EventFieldId,
                    Value = eventFieldValueDto.Value
                });
            }
        }

        var selectedEventDatesIds = viewModel.SelectedEventDatesIds;
        if (selectedEventDatesIds != null && selectedEventDatesIds.Count() > 0)
        {
            var registrations = new List<Registration>();
            foreach (var selectedEventDateId in selectedEventDatesIds)
            {
                var eventDate = e.EventDates.First(ed => ed.Id == selectedEventDateId);
                var timeSlot = eventDate.TimeSlot;
                if (timeSlot.AvailableSeats > 0)
                {
                    var registration = new Registration
                    {
                        Participant = mapper.Map<User>(viewModel.Participant),
                        TimeSlot = timeSlot,
                        TimeSlotId = timeSlot.Id,
                        EventFieldValues = newEventFieldValue,
                    };

                    var doomainTimeSlot = await appDbContext.TimeSlots.FirstAsync(ts => ts.EventDateId == selectedEventDateId);
                    doomainTimeSlot.AvailableSeats--;

                    registrationSuccesses.Add(new RegistrationSuccessDto
                    {
                        EventDate = eventDate,
                        IsSuccess = true,
                    });

                    registrations.Add(registration);
                }
                else
                {
                    registrationSuccesses.Add(new RegistrationSuccessDto
                    {
                        EventDate = eventDate,
                        IsSuccess = false,
                    });
                }
            }

            if (registrations.Count > 0)
            {
                await appDbContext.Registrations.AddRangeAsync(registrations);
            }
        }

        await appDbContext.EventFieldValues.AddRangeAsync(newEventFieldValue);

        await appDbContext.SaveChangesAsync();

        return registrationSuccesses;
    }
}
