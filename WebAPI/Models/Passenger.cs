using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Passenger
{
    public int PassengerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public int SegmentId { get; set; }

    public virtual Segment Segment { get; set; } = null!;
}
