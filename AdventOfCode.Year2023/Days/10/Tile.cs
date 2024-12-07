﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2023.Days._10
{
    public class Tile
    {
        public Tile(char id, Direction first, Direction second)
        {
            Identifier = id;
            DirectionOne = first;
            DirectionTwo = second;
        }
        public char Identifier { get; set; }
        public Direction DirectionOne { get; set; }
        public Direction DirectionTwo { get; set; }
    }

    public enum Direction
    {
        North,
        East,
        South,
        West
    }
}
