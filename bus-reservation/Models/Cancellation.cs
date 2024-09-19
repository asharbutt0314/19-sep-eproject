using System;
using System.Collections.Generic;

namespace bus_reservation.Models;

public partial class Cancellation
{
    public int CancellationId { get; set; }

    public int BookingId { get; set; }

    public DateTime CancellationDate { get; set; }

    public decimal RefundAmount { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
