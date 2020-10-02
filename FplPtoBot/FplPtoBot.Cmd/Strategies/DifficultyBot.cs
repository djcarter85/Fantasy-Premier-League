namespace FplPtoBot.Cmd.Strategies
{
    using FplPtoBot.Cmd.Model;

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

        public bool CanPredict(Season season)
        {
            return true;
        }

        public Score PredictScore(Fixture fixture, Season season)
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