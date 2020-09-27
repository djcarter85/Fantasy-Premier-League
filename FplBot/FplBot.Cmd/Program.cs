namespace FplBot.Cmd
{
    using System;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var predictionStrategies = new IPredictionStrategy[]
            {
                new NathanBot(),
            };

            var season = Season.Season1920;

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
        }
    }
}
