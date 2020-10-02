namespace FplBot.Cmd.Strategies
{
    using FplBot.Cmd.Model;

    public interface IPredictionStrategy
    {
        string Name { get; }

        bool CanPredict(Season season);

        Score PredictScore(Fixture fixture, Season season);
    }
}