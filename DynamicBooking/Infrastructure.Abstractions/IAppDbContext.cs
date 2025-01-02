
using DynamicBooking.Domain;
using DynamicBooking.Doomain;
using Microsoft.EntityFrameworkCore;

namespace DynamicBooking.Infrastructure.Abstractions;

public interface IAppDbContext
{
    DbSet<Event> Events { get; }

    DbSet<EventField> EventsFields { get; }

    DbSet<EventActionsId> EventActions { get; }

    DbSet<EventFieldValue> EventFieldValues { get; }

    DbSet<Registration> Registrations { get; }

    DbSet<EventFile> EventsFiles { get; }

    DbSet<User> Users { get; }

    DbSet<EventDate> EventsDate { get; }

    DbSet<TimeSlot> TimeSlots { get; }

    DbSet<RegistrationEventFieldValue> RegistrationEventFieldValues { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
