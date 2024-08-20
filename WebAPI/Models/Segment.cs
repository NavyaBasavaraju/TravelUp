using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Segment
{
    public int SegmentId { get; set; }

    public string FlightNumber { get; set; } = null!;

    public int DepartureCity { get; set; }

    public int ArrivalCity { get; set; }

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public virtual City ArrivalCityNavigation { get; set; } = null!;

    public virtual City DepartureCityNavigation { get; set; } = null!;

    public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
}
