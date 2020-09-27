namespace FplBot.Cmd
{
    public class DifficultyBot : IPredictionStrategy
    {
        private readonly Score homeWinScore;
        private readonly Score drawScore;
        private readonly Score awayWinScore;

        public DifficultyBot(Score homeWinScore, Score drawScore, Score awayWinScore)
        {
            this.homeWinScore = homeWinScore;
            this.drawScore = drawScore;
            this.awayWinScore = awayWinScore;
        }

        public string Name =>
            $"DifficultyBot {Display.Score(this.homeWinScore)} {Display.Score(this.drawScore)} {Display.Score(this.awayWinScore)}";

        public Score PredictScore(Fixture fixture)
        {
            if (fixture.HomeTeamDifficulty < fixture.AwayTeamDifficulty)
            {
                return this.homeWinScore;
            }

            if (fixture.AwayTeamDifficulty < fixture.HomeTeamDifficulty)
            {
                return this.awayWinScore;
            }

            return this.drawScore;
        }
    }
}