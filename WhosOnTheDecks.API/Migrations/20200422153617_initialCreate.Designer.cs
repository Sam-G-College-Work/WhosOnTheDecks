﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WhosOnTheDecks.API.Data;

namespace WhosOnTheDecks.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200422153617_initialCreate")]
    partial class initialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0");

            modelBuilder.Entity("WhosOnTheDecks.API.Models.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("BookingStatus")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DjId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("EventId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("BookingId");

                    b.HasIndex("DjId");

                    b.HasIndex("EventId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("WhosOnTheDecks.API.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("CostOfEvent")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfEvent")
                        .HasColumnType("TEXT");

                    b.Property<string>("EventAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EventEndTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EventStartTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("EventStatus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PromoterId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EventId");

                    b.HasIndex("PromoterId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("WhosOnTheDecks.API.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateMade")
                        .HasColumnType("TEXT");

                    b.Property<int>("EventId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("PaymentAmount")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PaymentStatus")
                        .HasColumnType("INTEGER");

                    b.HasKey("PaymentId");

                    b.HasIndex("EventId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("WhosOnTheDecks.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("LockAccount")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("WhosOnTheDecks.API.Models.Dj", b =>
                {
                    b.HasBaseType("WhosOnTheDecks.API.Models.User");

                    b.Property<string>("DjName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Equipment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Genre")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("HourlyRate")
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("Dj");
                });

            modelBuilder.Entity("WhosOnTheDecks.API.Models.Promoter", b =>
                {
                    b.HasBaseType("WhosOnTheDecks.API.Models.User");

                    b.Property<string>("CompanyName")
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("Promoter");
                });

            modelBuilder.Entity("WhosOnTheDecks.API.Models.Staff", b =>
                {
                    b.HasBaseType("WhosOnTheDecks.API.Models.User");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("Staff");
                });

            modelBuilder.Entity("WhosOnTheDecks.API.Models.Booking", b =>
                {
                    b.HasOne("WhosOnTheDecks.API.Models.Dj", "Dj")
                        .WithMany("Bookings")
                        .HasForeignKey("DjId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WhosOnTheDecks.API.Models.Event", "Event")
                        .WithMany("Bookings")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WhosOnTheDecks.API.Models.Event", b =>
                {
                    b.HasOne("WhosOnTheDecks.API.Models.Promoter", "Promoter")
                        .WithMany("Events")
                        .HasForeignKey("PromoterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WhosOnTheDecks.API.Models.Payment", b =>
                {
                    b.HasOne("WhosOnTheDecks.API.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
