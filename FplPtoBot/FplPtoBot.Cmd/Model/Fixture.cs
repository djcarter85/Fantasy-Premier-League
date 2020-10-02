namespace FplPtoBot.Cmd.Model
{
    public class Fixture
    {
        public Fixture(
            int homeTeamId,
            int awayTeamId,
            int homeTeamDifficulty,
            int awayTeamDifficulty)
        {
            this.HomeTeamId = homeTeamId;
            this.AwayTeamId = awayTeamId;
            this.HomeTeamDifficulty = homeTeamDifficulty;
            this.AwayTeamDifficulty = awayTeamDifficulty;
        }

        public int HomeTeamId { get; }

        public int AwayTeamId { get; }

        public int HomeTeamDifficulty { get; }

        public int AwayTeamDifficulty { get; }
    }
}