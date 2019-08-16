namespace Fpl
{
    public class DirectionalFixture
    {
        public int Gameweek { get; }
        public int TeamId { get; }
        public int OpponentId { get; }
        public MatchLocation Location { get; }
        public int Difficulty { get; }

        public DirectionalFixture(int gameweek, int teamId, int opponentId, MatchLocation location, int difficulty)
        {
            this.Gameweek = gameweek;
            this.TeamId = teamId;
            this.OpponentId = opponentId;
            this.Location = location;
            this.Difficulty = difficulty;
        }
    }
}