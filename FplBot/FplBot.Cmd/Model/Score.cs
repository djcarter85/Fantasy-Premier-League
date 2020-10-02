namespace FplBot.Cmd.Model
{
    public class Score
    {
        public Score(int home, int away)
        {
            this.Home = home;
            this.Away = away;
        }

        public int Home { get; }

        public int Away { get; }

        public MatchResult GetMatchResult()
        {
            if (this.Home > this.Away)
            {
                return MatchResult.HomeWin;
            }

            if (this.Away > this.Home)
            {
                return MatchResult.AwayWin;
            }

            return MatchResult.Draw;
        }
    }
}