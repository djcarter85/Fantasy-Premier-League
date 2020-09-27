﻿namespace FplBot.Cmd
{
    using System.Collections.Generic;
    using System.Linq;

    public class NathanBot : IPredictionStrategy
    {
        private const int LiverpoolId = 10;
        private const int ManCityId = 11;

        private static readonly IEnumerable<int> BigTeamIds = new[] { LiverpoolId, ManCityId };

        public string Name { get; } = "NathanBot";

        public Score PredictScore(Fixture fixture)
        {
            var homeTeamIsBig = BigTeamIds.Contains(fixture.HomeTeamId);
            var awayTeamIsBig = BigTeamIds.Contains(fixture.AwayTeamId);

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
    }
}