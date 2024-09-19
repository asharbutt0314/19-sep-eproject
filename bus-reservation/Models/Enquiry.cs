using System;
using System.Collections.Generic;

namespace bus_reservation.Models;

public partial class Enquiry
{
    public int EnquiryId { get; set; }

    public string? StartingPlace { get; set; }

    public string? DestinationPlace { get; set; }

    public DateTime? TravelDate { get; set; }

    public string Status { get; set; } = null!;
}
