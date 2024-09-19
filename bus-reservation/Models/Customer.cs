using System;
using System.Collections.Generic;

namespace bus_reservation.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public string? ContactNumber { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
