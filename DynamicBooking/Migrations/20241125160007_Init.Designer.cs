﻿// <auto-generated />
using System;
using DynamicBooking.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DynamicBooking.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241125160007_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("DynamicBooking.Domain.EventActionsId", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EditEventId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EventId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RegistrationEventId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ResultsId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EventId")
                        .IsUnique();

                    b.ToTable("EventActions");
                });

            modelBuilder.Entity("DynamicBooking.Domain.TimeRange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("TimeSlotId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TimeSlotId")
                        .IsUnique();

                    b.ToTable("TimeRanges");
                });

            modelBuilder.Entity("DynamicBooking.Doomain.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("DynamicBooking.Doomain.EventDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EventId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("EventsDate");
                });

            modelBuilder.Entity("DynamicBooking.Doomain.EventField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("EventId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("EventsFields");
                });

            modelBuilder.Entity("DynamicBooking.Doomain.EventFieldValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EventFieldId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EventFieldId");

                    b.ToTable("EventFieldValues");
                });

            modelBuilder.Entity("DynamicBooking.Doomain.EventFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EventId")
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("EventsFiles");
                });

            modelBuilder.Entity("DynamicBooking.Doomain.Registration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ParticipantId")
                        .HasColumnType("TEXT");

                    b.Property<int>("TimeSlotId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ParticipantId");

                    b.HasIndex("TimeSlotId");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("DynamicBooking.Doomain.TimeSlot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EventDateId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EventDateId");

                    b.ToTable("TimeSlots");
                });

            modelBuilder.Entity("DynamicBooking.Doomain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DynamicBooking.Domain.EventActionsId", b =>
                {
                    b.HasOne("DynamicBooking.Doomain.Event", "Event")
                        .WithOne("EventActions")
                        .HasForeignKey("DynamicBooking.Domain.EventActionsId", "EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("DynamicBooking.Domain.TimeRange", b =>
                {
                    b.HasOne("DynamicBooking.Doomain.TimeSlot", "TimeSlot")
                        .WithOne("TimeRange")
                        .HasForeignKey("DynamicBooking.Domain.TimeRange", "TimeSlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TimeSlot");
                });

            modelBuilder.Entity("DynamicBooking.Doomain.Event", b =>
                {
                    b.HasOne("DynamicBooking.Doomain.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("DynamicBooking.Doomain.EventDate", b =>
                {
                    b.HasOne("DynamicBooking.Doomain.Event", "Event")
                        .WithMany("EventDates")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("DynamicBooking.Doomain.EventField", b =>
                {
                    b.HasOne("DynamicBooking.Doomain.Event", "Event")
                        .WithMany("OptionalFields")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("DynamicBooking.Doomain.EventFieldValue", b =>
                {
                    b.HasOne("DynamicBooking.Doomain.EventField", "EventField")
                        .WithMany("EventFieldValues")
                        .HasForeignKey("EventFieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventField");
                });

            modelBuilder.Entity("DynamicBooking.Doomain.EventFile", b =>
                {
                    b.HasOne("DynamicBooking.Doomain.Event", "Event")
                        .WithMany("FormFiles")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("DynamicBooking.Doomain.Registration", b =>
                {
                    b.HasOne("DynamicBooking.Doomain.User", "Participant")
                        .WithMany()
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DynamicBooking.Doomain.TimeSlot", "TimeSlot")
                        .WithMany("Registrations")
                        .HasForeignKey("TimeSlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Participant");

                    b.Navigation("TimeSlot");
                });

            modelBuilder.Entity("DynamicBooking.Doomain.TimeSlot", b =>
                {
                    b.HasOne("DynamicBooking.Doomain.EventDate", "EventDate")
                        .WithMany("TimeSlots")
                        .HasForeignKey("EventDateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventDate");
                });

            modelBuilder.Entity("DynamicBooking.Doomain.Event", b =>
                {
                    b.Navigation("EventActions")
                        .IsRequired();

                    b.Navigation("EventDates");

                    b.Navigation("FormFiles");

                    b.Navigation("OptionalFields");
                });

            modelBuilder.Entity("DynamicBooking.Doomain.EventDate", b =>
                {
                    b.Navigation("TimeSlots");
                });

            modelBuilder.Entity("DynamicBooking.Doomain.EventField", b =>
                {
                    b.Navigation("EventFieldValues");
                });

            modelBuilder.Entity("DynamicBooking.Doomain.TimeSlot", b =>
                {
                    b.Navigation("Registrations");

                    b.Navigation("TimeRange")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
