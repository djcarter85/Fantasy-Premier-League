namespace FplPtoBot.Cmd
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FplPtoBot.Cmd.Calculations;
    using FplPtoBot.Cmd.Model;
    using FplPtoBot.Cmd.Repositories;
    using FplPtoBot.Cmd.Strategies;

    public static class Program
    {
        private static readonly IEnumerable<IPredictionStrategy> PredictionStrategies = new IPredictionStrategy[]
        {
            new OldNathanBot(),
            new NewNathanBot(),
            new DifficultyBot(win: new DirectionalScore(2, 1), draw: new DirectionalScore(1, 1)),
        };

        public static void Main(string[] args)
        {
            SimulateGameweek(Season.Season2021, 3);
        }

        private static void SimulateGameweek(Season season, int gameweekNumber)
        {
            var fixtures = FixtureRepository.GetAllFixtures(season, gameweekNumber);
            var teams = TeamRepository.GetAllTeams(season);

            foreach (var predictionStrategy in PredictionStrategies.Where(ps=>ps.CanPredict(season)))
            {
                Console.WriteLine(predictionStrategy.Name);

                foreach (var fixture in fixtures)
                {
                    var homeTeam = teams[fixture.HomeTeamId];
                    var awayTeam = teams[fixture.AwayTeamId];

                    var predicted = predictionStrategy.PredictScore(fixture, season);

                    Console.WriteLine($"{homeTeam.ShortName} {Display.Score(predicted)} {awayTeam.ShortName}");
                }

                Console.WriteLine();
            }
        }

        private static void RunSeasonPredictions()
        {
            var seasons = new[]
            {
                Season.Season1819,
                Season.Season1920,
                Season.Season2021,
            };

            foreach (var season in seasons)
            {
                Console.WriteLine(Display.Season(season));

                var fixtures = FixtureRepository.GetAllCompletedFixtures(season);

                foreach (var predictionStrategy in PredictionStrategies)
                {
                    if (predictionStrategy.CanPredict(season))
                    {
                        var totalPoints = 0;

                        foreach (var fixture in fixtures)
                        {
                            var predicted = predictionStrategy.PredictScore(fixture, season);

                            var points = PointsCalculator.CalculatePoints(predicted, fixture.FinalScore);

                            totalPoints += points;
                        }

                        Console.WriteLine($"{predictionStrategy.Name}: {totalPoints}");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
