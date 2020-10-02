namespace FplPtoBot.Cmd.Model
{
    public class CompletedFixture : Fixture
    {
        public CompletedFixture(
            int homeTeamId,
            int awayTeamId,
            int homeTeamDifficulty,
            int awayTeamDifficulty,
            Score finalScore)
            : base(
                homeTeamId: homeTeamId,
                awayTeamId: awayTeamId,
                homeTeamDifficulty: homeTeamDifficulty,
                awayTeamDifficulty: awayTeamDifficulty)
        {
            this.FinalScore = finalScore;
        }

        public Score FinalScore { get; }
    }
}