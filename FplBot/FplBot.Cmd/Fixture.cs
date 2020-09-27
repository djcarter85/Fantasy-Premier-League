namespace FplBot.Cmd
{
    public class Fixture
    {
        public Fixture(
            int homeTeamId,
            int awayTeamId,
            int homeTeamDifficulty,
            int awayTeamDifficulty,
            Score finalScore)
        {
            this.HomeTeamId = homeTeamId;
            this.AwayTeamId = awayTeamId;
            this.HomeTeamDifficulty = homeTeamDifficulty;
            this.AwayTeamDifficulty = awayTeamDifficulty;
            this.FinalScore = finalScore;
        }

        public int HomeTeamId { get; }

        public int AwayTeamId { get; }

        public int HomeTeamDifficulty { get; }

        public int AwayTeamDifficulty { get; }

        public Score FinalScore { get; }
    }
}