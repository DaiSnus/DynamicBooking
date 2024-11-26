using AutoMapper;
using DynamicBooking.Domain;
using DynamicBooking.Doomain;
using DynamicBooking.UseCases.GetEvent;
using DynamicBooking.UseCases.Signup;

namespace DynamicBooking.UseCases;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<EventDto, Event>();
        CreateMap<EventFileDto, EventFile>();
        CreateMap<EventDateDto, EventDate>();
        CreateMap<EventFieldDto, EventField>();
        CreateMap<TimeSlotDto, TimeSlot>();
        CreateMap<UserDto, User>();
        CreateMap<EventActionsIdDto, EventActionsId>();
        CreateMap<RegistrationDto, Registration>();
        CreateMap<EventFieldValueDto, EventFieldValue>();
        CreateMap<TimeRangeDto, TimeRange>();

        CreateMap<Event, EventDto>();
        CreateMap<EventFile, EventFileDto>();
        CreateMap<EventDate, EventDateDto>();
        CreateMap<EventField, EventFieldDto>();
        CreateMap<TimeSlot, TimeSlotDto>();
        CreateMap<User, UserDto>();
        CreateMap<EventActionsId, EventActionsIdDto>();
        CreateMap<Registration, RegistrationDto>();
        CreateMap<EventFieldValue, EventFieldValueDto>();
        CreateMap<TimeRange, TimeRangeDto>();
    }
}
