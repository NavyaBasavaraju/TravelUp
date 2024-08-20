using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class City
{
    public int CityId { get; set; }

    public string CityName { get; set; } = null!;

    public virtual ICollection<Segment> SegmentArrivalCityNavigations { get; set; } = new List<Segment>();

    public virtual ICollection<Segment> SegmentDepartureCityNavigations { get; set; } = new List<Segment>();
}
