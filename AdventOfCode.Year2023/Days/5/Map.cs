﻿using AdventOfCode.Year2023.Enums;

namespace AdventOfCode.Year2023.Days.DayFive;
public class Map
{
    public MapType SourceType { get; set; }
    public MapType DestinationType { get; set; }
    public IList<ObjectToObjectRange> Ranges { get; set; } = new List<ObjectToObjectRange>();
}
