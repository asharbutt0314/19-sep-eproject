using System;
using System.Collections.Generic;

namespace bus_reservation.Models;

public partial class BusSeat
{
    public int SeatId { get; set; }

    public int BusId { get; set; }

    public string SeatNumber { get; set; } = null!;

    public bool? IsAvailable { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Bus Bus { get; set; } = null!;
}
