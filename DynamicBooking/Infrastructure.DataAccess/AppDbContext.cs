using DynamicBooking.Domain;
using DynamicBooking.Doomain;
using DynamicBooking.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DynamicBooking.Infrastructure.DataAccess;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Event> Events { get; private set; }

    public DbSet<User> Users { get; private set; }

    public DbSet<EventActionsId> EventActions { get; private set; }

    public DbSet<EventDate> EventsDate { get; private set; }

    public DbSet<TimeSlot> TimeSlots { get; private set; }

    public DbSet<EventField> EventsFields { get; private set; }

    public DbSet<EventFieldValue> EventFieldValues { get; private set; }

    public DbSet<Registration> Registrations { get; private set; }

    public DbSet<EventFile> EventsFiles { get; private set; }

    public DbSet<TimeRange> TimeRanges { get; private set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>()
                .HasOne(e => e.Owner)
                .WithMany();

        modelBuilder.Entity<EventActionsId>()
                .HasOne(ea => ea.Event)
                .WithOne(e => e.EventActions)
                .HasForeignKey<EventActionsId>(ea => ea.EventId);

        modelBuilder.Entity<EventDate>()
                .HasOne(ed => ed.Event)
                .WithMany(e => e.EventDates)
                .HasForeignKey(eb => eb.EventId);

        modelBuilder.Entity<TimeSlot>()
                .HasOne(ts => ts.EventDate)
                .WithOne(ed => ed.TimeSlot)
                .HasForeignKey<TimeSlot>(ts => ts.EventDateId);

        modelBuilder.Entity<TimeRange>()
                .HasOne(tr => tr.TimeSlot)
                .WithOne(ts => ts.TimeRange)
                .HasForeignKey<TimeRange>(tr => tr.TimeSlotId);

        modelBuilder.Entity<EventFile>()
                .HasOne(eventFile => eventFile.Event)
                .WithMany(e => e.FormFiles)
                .HasForeignKey(eventFile => eventFile.EventId);

        modelBuilder.Entity<EventField>()
                .HasOne(eventField => eventField.Event)
                .WithMany(e => e.OptionalFields)
                .HasForeignKey(eventField => eventField.EventId);

        modelBuilder.Entity<EventFieldValue>()
                .HasOne(eventFieldValue => eventFieldValue.EventField)
                .WithMany(eventField => eventField.EventFieldValues)
                .HasForeignKey(eventFieldValue => eventFieldValue.EventFieldId);

        modelBuilder.Entity<Registration>()
                .HasOne(r => r.TimeSlot)
                .WithMany(ts => ts.Registrations)
                .HasForeignKey(r => r.TimeSlotId);

        modelBuilder.Entity<Registration>()
                .HasOne(r => r.Participant)
                .WithMany()
                .HasForeignKey(r => r.ParticipantId);
    }
}
