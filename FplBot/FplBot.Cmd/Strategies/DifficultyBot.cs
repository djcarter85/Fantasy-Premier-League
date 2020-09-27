namespace FplBot.Cmd.Strategies
{
    using FplBot.Cmd.Model;

    public class DifficultyBot : IPredictionStrategy
    {
        private readonly DirectionalScore win;
        private readonly DirectionalScore draw;

        public DifficultyBot(DirectionalScore win, DirectionalScore draw)
        {
            this.win = win;
            this.draw = draw;
        }

        public string Name =>
            $"DifficultyBot {Display.DirectionalScore(this.win)} {Display.DirectionalScore(this.draw)}";

        public Score PredictScore(Fixture fixture)
        {
            if (fixture.HomeTeamDifficulty < fixture.AwayTeamDifficulty)
            {
                return this.win.ToScore(MatchResult.HomeWin);
            }

            if (fixture.AwayTeamDifficulty < fixture.HomeTeamDifficulty)
            {
                return this.win.ToScore(MatchResult.AwayWin);
            }

            return this.draw.ToScore(MatchResult.Draw);
        }
    }
}