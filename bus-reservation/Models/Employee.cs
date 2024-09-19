using System;
using System.Collections.Generic;

namespace bus_reservation.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string Username { get; set; } = null!;

    public string EmployeeEmail { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? ContactNumber { get; set; }

    public string? Location { get; set; }

    public string? Qualification { get; set; }

    public int Age { get; set; }
}
