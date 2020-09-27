namespace FplBot.Cmd
{
    using System;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var predictionStrategies = new IPredictionStrategy[]
            {
                new OldNathanBot(),
                new DifficultyBot(homeWinScore: new Score(1, 0), drawScore: new Score(0, 0), awayWinScore: new Score(0, 1)),
                new DifficultyBot(homeWinScore: new Score(2, 0), drawScore: new Score(0, 0), awayWinScore: new Score(0, 2)),
                new DifficultyBot(homeWinScore: new Score(2, 1), drawScore: new Score(0, 0), awayWinScore: new Score(1, 2)),
                new DifficultyBot(homeWinScore: new Score(1, 0), drawScore: new Score(1, 1), awayWinScore: new Score(0, 1)),
                new DifficultyBot(homeWinScore: new Score(1, 0), drawScore: new Score(2, 2), awayWinScore: new Score(0, 1)),
            };

            var seasons = new[]
            {
                Season.Season1819,
                Season.Season1920,
            };

            foreach (var season in seasons)
            {
                Console.WriteLine(Display.Season(season));

                var fixtures = new FixtureRepository().GetAllFixtures(season);

                foreach (var predictionStrategy in predictionStrategies)
                {
                    var totalPoints = 0;

                    foreach (var fixture in fixtures)
                    {
                        var predicted = predictionStrategy.PredictScore(fixture);

                        var points = PointsCalculator.CalculatePoints(predicted, fixture.FinalScore);

                        totalPoints += points;
                    }

                    Console.WriteLine($"{predictionStrategy.Name}: {totalPoints}");
                }

                Console.WriteLine();
            }
        }
    }
}
