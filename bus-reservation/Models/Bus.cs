using System;
using System.Collections.Generic;

namespace bus_reservation.Models;

public partial class Bus
{
    public int BusId { get; set; }

    public string BusNumber { get; set; } = null!;

    public int BusTypeId { get; set; }

    public string? Route { get; set; }

    public string? StartingPlace { get; set; }

    public string? DestinationPlace { get; set; }

    public int TotalSeats { get; set; }

    public int AvailableSeats { get; set; }

    public TimeSpan? DepartureTime { get; set; }

    public TimeSpan? ArrivalTime { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<BusSeat> BusSeats { get; set; } = new List<BusSeat>();

    public virtual BusType BusType { get; set; } = null!;
}
