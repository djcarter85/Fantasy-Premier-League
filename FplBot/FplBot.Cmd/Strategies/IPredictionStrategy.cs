namespace FplBot.Cmd.Strategies
{
    using FplBot.Cmd.Model;

    public interface IPredictionStrategy
    {
        string Name { get; }

        Score PredictScore(Fixture fixture);
    }
}