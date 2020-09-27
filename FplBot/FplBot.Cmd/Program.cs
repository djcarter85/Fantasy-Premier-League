namespace FplBot.Cmd
{
    using System;
    using FplBot.Cmd.Calculations;
    using FplBot.Cmd.Model;
    using FplBot.Cmd.Repositories;
    using FplBot.Cmd.Strategies;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var predictionStrategies = new IPredictionStrategy[]
            {
                new OldNathanBot(),
                new NewNathanBot(),
                new DifficultyBot(win: new DirectionalScore(2, 1), draw: new DirectionalScore(1, 1)),
            };

            var seasons = new[]
            {
                Season.Season1819,
                Season.Season1920,
                Season.Season2021,
            };

            foreach (var season in seasons)
            {
                Console.WriteLine(Display.Season(season));

                var fixtures = FixtureRepository.GetAllFixtures(season);

                foreach (var predictionStrategy in predictionStrategies)
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
