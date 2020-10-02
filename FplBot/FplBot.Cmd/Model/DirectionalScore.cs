namespace FplBot.Cmd.Model
{
    using System;

    public class DirectionalScore
    {
        public DirectionalScore(int winningScore, int losingScore)
        {
            this.WinningScore = winningScore;
            this.LosingScore = losingScore;
        }

        public int WinningScore { get; }
        
        public int LosingScore { get; }

        public Score ToScore(MatchResult matchResult)
        {
            return matchResult switch
            {
                MatchResult.HomeWin when this.WinningScore > this.LosingScore => new Score(this.WinningScore, this.LosingScore),
                MatchResult.HomeWin => throw new InvalidOperationException(),
                MatchResult.AwayWin when this.WinningScore > this.LosingScore => new Score(this.LosingScore, this.WinningScore),
                MatchResult.AwayWin => throw new InvalidOperationException(),
                MatchResult.Draw when this.WinningScore == this.LosingScore => new Score(this.WinningScore, this.LosingScore),
                MatchResult.Draw => throw new InvalidOperationException(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}