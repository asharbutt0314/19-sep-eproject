using System;
using System.Collections.Generic;

namespace bus_reservation.Models;

public partial class Pricing
{
    public int PricingId { get; set; }

    public int BusTypeId { get; set; }

    public decimal PricePerSeat { get; set; }

    public decimal DistanceRate { get; set; }

    public virtual BusType BusType { get; set; } = null!;
}
