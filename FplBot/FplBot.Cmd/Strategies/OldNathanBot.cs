namespace FplBot.Cmd.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FplBot.Cmd.Model;

    public class OldNathanBot : IPredictionStrategy
    {
        public string Name { get; } = "Old NathanBot";

        public bool CanPredict(Season season)
        {
            return season == Season.Season1920 || season == Season.Season2021;
        }

        public Score PredictScore(Fixture fixture, Season season)
        {
            var homeTeamIsBig = TeamIsBig(fixture.HomeTeamId, season);
            var awayTeamIsBig = TeamIsBig(fixture.AwayTeamId, season);

            if (homeTeamIsBig && awayTeamIsBig)
            {
                return new Score(1, 0);
            }

            if (homeTeamIsBig)
            {
                return new Score(2, 0);
            }

            if (awayTeamIsBig)
            {
                return new Score(0, 1);
            }

            return new Score(1, 0);
        }

        private static bool TeamIsBig(int teamId, Season season)
        {
            if (season == Season.Season1920)
            {
                var bigTeamIds = new[]
                {
                    10, // LIV 
                    11, // MCI
                };

                return bigTeamIds.Contains(teamId);
            }

            if (season == Season.Season2021)
            {
                var bigTeamIds = new[]
                {
                    11, // LIV 
                    12, // MCI
                };

                return bigTeamIds.Contains(teamId);
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}