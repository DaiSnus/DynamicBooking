using AutoMapper;
using DynamicBooking.Domain;
using DynamicBooking.Doomain;
using DynamicBooking.UseCases.GetEvent;

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
        CreateMap<EventActionsDto, EventActions>();
    }
}
