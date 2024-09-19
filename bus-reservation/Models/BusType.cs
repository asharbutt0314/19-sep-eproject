using System;
using System.Collections.Generic;

namespace bus_reservation.Models;

public partial class BusType
{
    public int BusTypeId { get; set; }

    public string BusTypeName { get; set; } = null!;

    public virtual ICollection<Bus> Buses { get; set; } = new List<Bus>();

    public virtual ICollection<Pricing> Pricings { get; set; } = new List<Pricing>();
}
