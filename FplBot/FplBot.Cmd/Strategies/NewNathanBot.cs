namespace FplBot.Cmd.Strategies
{
    using System;
    using FplBot.Cmd.Model;

    public class NewNathanBot : IPredictionStrategy
    {
        public string Name { get; } = "New Nathan Bot";

        public bool CanPredict(Season season)
        {
            return season == Season.Season1920 || season == Season.Season2021;
        }

        public Score PredictScore(Fixture fixture, Season season)
        {
            var homeTeamStrength = GetStrength(fixture.HomeTeamId, season);
            var awayTeamStrength = GetStrength(fixture.AwayTeamId, season);

            if (homeTeamStrength > awayTeamStrength)
            {
                return new Score(2, 1);
            }

            if (awayTeamStrength > homeTeamStrength)
            {
                return new Score(1, 2);
            }

            return new Score(1, 0);
        }

        private static int GetStrength(int teamId, Season season)
        {
            if (season == Season.Season1920)
            {
                return teamId switch
                {
                    1 => 3, // ARS
                    2 => 1, // AVL
                    3 => 1, // BOU
                    4 => 1, // BRI
                    5 => 1, // BUR
                    6 => 3, // CHE
                    7 => 1, // CRY
                    8 => 2, // EVE
                    9 => 2, // LEI
                    10 => 4, // LIV
                    11 => 4, // MCI
                    12 => 3, // MUN
                    13 => 1, // NEW
                    14 => 1, // NOR
                    15 => 1, // SHU
                    16 => 1, // SOU
                    17 => 3, // TOT
                    18 => 1, // WAT
                    19 => 2, // WHU
                    20 => 2, // WOL
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            if (season == Season.Season2021)
            {
                return teamId switch
                {
                    1 => 2, // ARS
                    2 => 1, // AVL
                    3 => 1, // BRI
                    4 => 2, // BUR
                    5 => 3, // CHE
                    6 => 1, // CRY
                    7 => 1, // EVE
                    8 => 1, // FUL
                    9 => 3, // LEI
                    10 => 1, // LEE
                    11 => 4, // LIV
                    12 => 4, // MCI
                    13 => 3, // MUN
                    14 => 1, // NEW
                    15 => 2, // SHU
                    16 => 1, // SOU
                    17 => 3, // TOT
                    18 => 1, // WBA
                    19 => 1, // WHU
                    20 => 2, // WOL
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            throw new NotImplementedException();

        }
    }
}