﻿using System.Text.RegularExpressions;

namespace AdventOfCode.Days.DayTwo;
public class DayTwoMain : AdventOfCodeDay
{
    private const int _day = 2;
    private const bool _debugging = false;

    public DayTwoMain() : base(_day, _debugging) { }
    public override async Task Run()
    {

        List<Game> GamesPlayed = new();
        var linesOfInput = await LoadFile();
        foreach (var line in linesOfInput)
        {
            var gameRecord = line.Split(':');
            if (gameRecord.Length == 2)
            {
                var identifier = gameRecord.First();
                var results = gameRecord.Last();

                var game = new Game();
                game.Id = int.Parse(Regex.Match(identifier, "\\d+").Value);
                foreach (var result in results.Split(';'))
                {
                    var resultRecord = new Result();
                    foreach (var score in result.Split(','))
                    {
                        var scoreParts = score.Trim().Split(' ');
                        var count = int.Parse(scoreParts.First());
                        var colour = scoreParts.Last();

                        switch (colour.Trim().ToLower())
                        {
                            case "blue":
                                resultRecord.Blue = count;
                                break;
                            case "red":
                                resultRecord.Red = count;
                                break;
                            case "green":
                                resultRecord.Green = count;
                                break;
                        }
                    }
                    game.Results.Add(resultRecord);
                }

                GamesPlayed.Add(game);
            }
            else throw new ArgumentException("Line format could not be determined");
        }

        //Params
        int maxRed = 12;
        int maxGreen = 13;
        int maxBlue = 14;

        var possibleGames = GamesPlayed.Where(g => g.Results.All(r => r.Red <= maxRed && r.Green <= maxGreen && r.Blue <= maxBlue)).ToList();

        Part1Result = possibleGames.Sum(pg => pg.Id);
        Part2Result = GamesPlayed.Sum(pg => pg.Power);
    }
}