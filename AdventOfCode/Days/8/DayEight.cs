﻿using AdventOfCode.Enums;

namespace AdventOfCode.Days.DayEight;
public class Day8 : AdventOfCodeDay
{
    private const bool _debugging = true;
    public Day8() : base(Day.Eight, _debugging) { }

    public override async Task Run()
    {
        var linesOfInput = await LoadFile();
        List<char> directions = linesOfInput.First().ToUpper().ToList();
        List<MapNode> nodes = new();
        foreach (var line in linesOfInput.Skip(2))
        {
            var mapNode = new MapNode();

            var parts = line.Split('=');
            mapNode.CurrentPosition = parts.First().Trim().ToUpper();

            var leftRight = parts.Last().Split(',');
            mapNode.Left = leftRight.First().Trim().TrimStart('(').Trim().ToUpper();
            mapNode.Right = leftRight.Last().Trim().TrimEnd(')').Trim().ToUpper();

            nodes.Add(mapNode);
        }

        //Get start position
        int steps = 0;
        int index = 0;
        var currentNode = nodes.Single(n => n.CurrentPosition == "AAA");
        while (currentNode.CurrentPosition != "ZZZ")
        {
            var direction = directions[index];
            if (direction == 'L')
            {
                WriteLine($"(Step {steps}) I'm at {currentNode.CurrentPosition} going {direction} to {currentNode.Left}");
                currentNode = nodes.Single(n => n.CurrentPosition == currentNode.Left);
            }
            else if (direction == 'R')
            {
                WriteLine($"(Step {steps}) I'm at {currentNode.CurrentPosition} going {direction} to {currentNode.Right}");
                currentNode = nodes.Single(n => n.CurrentPosition == currentNode.Right);
            }
            else
            {
                throw new DataMisalignedException("Expected 'L' or 'R' and DIDNT");
            }
            steps++;
            index++;
            if (index >= directions.Count) index = 0;
        }
        SetResult1(steps);

        //Part2
        var traversePaths = new List<TraversalPath>();
        var startNodes = nodes.Where(n => n.CurrentPosition.EndsWith("A")).ToList();
        foreach (var node in startNodes)
        {
            var traversal = new TraversalPath
            {
                StartPosition = node.CurrentPosition
            };

            steps = 0;
            index = 0;
            currentNode = node;
            while (traversal.StepsToRepeat == 0)
            {
                var direction = directions[index];
                if (direction == 'L')
                {
                    WriteLine($"(Step {steps}) I'm at {currentNode.CurrentPosition} going {direction} to {currentNode.Left}");
                    currentNode = nodes.Single(n => n.CurrentPosition == currentNode.Left);
                }
                else if (direction == 'R')
                {
                    WriteLine($"(Step {steps}) I'm at {currentNode.CurrentPosition} going {direction} to {currentNode.Right}");
                    currentNode = nodes.Single(n => n.CurrentPosition == currentNode.Right);
                }

                steps++;
                index++;
                if (index >= directions.Count) index = 0;
                if (currentNode.CurrentPosition.EndsWith("Z"))
                {
                    if (traversal.StepsToEnd == 0)
                    {
                        traversal.StepsToEnd = steps;
                        traversal.EndPosition = currentNode.CurrentPosition;
                        steps = 0;
                    }
                    else if (traversal.StepsToEnd == steps)
                    {
                        traversal.StepsToRepeat = steps;
                    }
                    else
                    {
                        throw new ArithmeticException("This pattern doesn't repeat dipshit");
                    }
                }
            }
            traversePaths.Add(traversal);
        }

        //Now work out the lowest common factor I guess
        long lowestCommonFactor = FactorsOfSet(traversePaths.Select(f => f.StepsToRepeat));

        SetResult2(lowestCommonFactor);
        await base.Run();
    }

    private long FactorsOfSet(IEnumerable<long> factors)
    {
        List<long> lowestFactors = new();
        foreach (var factor in factors)
        {
            lowestFactors.AddRange(factors.Where(f => f != factor).Select(otherFactor => LowestCommonFactor(factor, otherFactor)));
        }

        var distinctFactors = lowestFactors.Distinct().ToList();
        if (distinctFactors.Count > 1)
        {
            return FactorsOfSet(distinctFactors);
        }
        else if (distinctFactors.Count == 1)
        {
            return distinctFactors.First();
        }
        throw new ArithmeticException("Failed to do maths");
    }

    private long LowestCommonFactor(long a, long b)
    {
        long higher = Math.Max(a, b);
        long lower = Math.Min(a, b);

        int lowPow = 1;
        int highPow = 1;

        long factor = 0;
        while (factor == 0)
        {
            if (higher * highPow == lower * lowPow)
            { 
                factor = (higher*highPow);
            }
            else
            {
                lowPow++;
                if (lower*lowPow > higher*highPow)
                {
                    highPow++;
                }
            }
        }
        return factor;
    }
}
