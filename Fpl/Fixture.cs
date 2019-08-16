namespace Fpl
{
    public class Fixture
    {
        public int Gameweek { get; }
        public int HomeTeamId { get; }
        public int AwayTeamId { get; }
        public int HomeTeamDifficulty { get; }
        public int AwayTeamDifficulty { get; }

        public Fixture(int gameweek, int homeTeamId, int awayTeamId, int homeTeamDifficulty, int awayTeamDifficulty)
        {
            this.Gameweek = gameweek;
            this.HomeTeamId = homeTeamId;
            this.AwayTeamId = awayTeamId;
            this.HomeTeamDifficulty = homeTeamDifficulty;
            this.AwayTeamDifficulty = awayTeamDifficulty;
        }
    }
}