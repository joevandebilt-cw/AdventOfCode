﻿using AdventOfCode.Enums;

namespace AdventOfCode.Days.Template;
public class Day0 : AdventOfCodeDay
{
    private const bool _debugging = false;
    public Day0() : base(Day.Zero, _debugging) { }

    public override async Task Run()
    {
        Part1Result = -1;
        Part2Result = -1;
        await Task.CompletedTask;
    }
}
