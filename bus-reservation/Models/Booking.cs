using System;
using System.Collections.Generic;

namespace bus_reservation.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int CustomerId { get; set; }

    public int BusId { get; set; }

    public int SeatId { get; set; }

    public DateTime BookingDate { get; set; }

    public DateTime TravelDate { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual Bus Bus { get; set; } = null!;

    public virtual ICollection<Cancellation> Cancellations { get; set; } = new List<Cancellation>();

    public virtual Customer Customer { get; set; } = null!;

    public virtual BusSeat Seat { get; set; } = null!;
}
