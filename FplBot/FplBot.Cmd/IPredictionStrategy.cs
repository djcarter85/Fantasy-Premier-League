namespace FplBot.Cmd
{
    public interface IPredictionStrategy
    {
        string Name { get; }

        Score PredictScore(Fixture fixture);
    }
}