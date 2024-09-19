using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace bus_reservation.Models;

public partial class BusReservationContext : DbContext
{
    public BusReservationContext()
    {
    }

    public BusReservationContext(DbContextOptions<BusReservationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Bus> Buses { get; set; }

    public virtual DbSet<BusSeat> BusSeats { get; set; }

    public virtual DbSet<BusType> BusTypes { get; set; }

    public virtual DbSet<Cancellation> Cancellations { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Enquiry> Enquiries { get; set; }

    public virtual DbSet<Pricing> Pricings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("data source=.;initial catalog=OnlineBusTicketReservation;user id=sa;password=aptech; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__5DE3A5B1DAD43501");

            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.BookingDate)
                .HasColumnType("date")
                .HasColumnName("booking_date");
            entity.Property(e => e.BusId).HasColumnName("bus_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.SeatId).HasColumnName("seat_id");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_amount");
            entity.Property(e => e.TravelDate)
                .HasColumnType("date")
                .HasColumnName("travel_date");

            entity.HasOne(d => d.Bus).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.BusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__bus_id__38996AB5");

            entity.HasOne(d => d.Customer).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__custom__37A5467C");

            entity.HasOne(d => d.Seat).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.SeatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__seat_i__398D8EEE");
        });

        modelBuilder.Entity<Bus>(entity =>
        {
            entity.HasKey(e => e.BusId).HasName("PK__Buses__6ACEF8ED78B8C095");

            entity.HasIndex(e => e.BusNumber, "UQ__Buses__0D3182B93E9131B4").IsUnique();

            entity.Property(e => e.BusId).HasColumnName("bus_id");
            entity.Property(e => e.ArrivalTime).HasColumnName("arrival_time");
            entity.Property(e => e.AvailableSeats).HasColumnName("available_seats");
            entity.Property(e => e.BusNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("bus_number");
            entity.Property(e => e.BusTypeId).HasColumnName("bus_type_id");
            entity.Property(e => e.DepartureTime).HasColumnName("departure_time");
            entity.Property(e => e.DestinationPlace)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("destination_place");
            entity.Property(e => e.Route)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("route");
            entity.Property(e => e.StartingPlace)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("starting_place");
            entity.Property(e => e.TotalSeats).HasColumnName("total_seats");

            entity.HasOne(d => d.BusType).WithMany(p => p.Buses)
                .HasForeignKey(d => d.BusTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Buses__bus_type___286302EC");
        });

        modelBuilder.Entity<BusSeat>(entity =>
        {
            entity.HasKey(e => e.SeatId).HasName("PK__BusSeats__906DED9CC7B3C958");

            entity.HasIndex(e => new { e.BusId, e.SeatNumber }, "UQ__BusSeats__75C2413746AE1B34").IsUnique();

            entity.Property(e => e.SeatId).HasColumnName("seat_id");
            entity.Property(e => e.BusId).HasColumnName("bus_id");
            entity.Property(e => e.IsAvailable)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("is_available");
            entity.Property(e => e.SeatNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("seat_number");

            entity.HasOne(d => d.Bus).WithMany(p => p.BusSeats)
                .HasForeignKey(d => d.BusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BusSeats__bus_id__34C8D9D1");
        });

        modelBuilder.Entity<BusType>(entity =>
        {
            entity.HasKey(e => e.BusTypeId).HasName("PK__BusTypes__22C0FC63B71E555C");

            entity.HasIndex(e => e.BusTypeName, "UQ__BusTypes__BB856E1BDE56011D").IsUnique();

            entity.Property(e => e.BusTypeId).HasColumnName("bus_type_id");
            entity.Property(e => e.BusTypeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("bus_type_name");
        });

        modelBuilder.Entity<Cancellation>(entity =>
        {
            entity.HasKey(e => e.CancellationId).HasName("PK__Cancella__4ED4366D7B63E4B2");

            entity.Property(e => e.CancellationId).HasColumnName("cancellation_id");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.CancellationDate)
                .HasColumnType("date")
                .HasColumnName("cancellation_date");
            entity.Property(e => e.RefundAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("refund_amount");

            entity.HasOne(d => d.Booking).WithMany(p => p.Cancellations)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cancellat__booki__3C69FB99");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__CD65CB85A26C38B5");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contact_number");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__C52E0BA88FB85C40");

            entity.HasIndex(e => e.Username, "UQ__Employee__F3DBC572D53CAF26").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contact_number");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Qualification)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("qualification");
        });

        modelBuilder.Entity<Enquiry>(entity =>
        {
            entity.HasKey(e => e.EnquiryId).HasName("PK__Enquirie__57CC01B387ABD8CF");

            entity.Property(e => e.EnquiryId).HasColumnName("enquiry_id");
            entity.Property(e => e.DestinationPlace)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("destination_place");
            entity.Property(e => e.StartingPlace)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("starting_place");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.TravelDate)
                .HasColumnType("date")
                .HasColumnName("travel_date");
        });

        modelBuilder.Entity<Pricing>(entity =>
        {
            entity.HasKey(e => e.PricingId).HasName("PK__Pricing__A25A9FB72AB59F3E");

            entity.ToTable("Pricing");

            entity.Property(e => e.PricingId).HasColumnName("pricing_id");
            entity.Property(e => e.BusTypeId).HasColumnName("bus_type_id");
            entity.Property(e => e.DistanceRate)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("distance_rate");
            entity.Property(e => e.PricePerSeat)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price_per_seat");

            entity.HasOne(d => d.BusType).WithMany(p => p.Pricings)
                .HasForeignKey(d => d.BusTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pricing__bus_typ__2B3F6F97");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
