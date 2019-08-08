namespace Fpl
{
    using System.Collections.Generic;
    using System.Linq;
    using MoreLinq;

    public class StartingEleven
    {
        public IReadOnlyList<Player> Players { get; }
        public Formation Formation { get; }
        public int TotalPoints => this.Players.Sum(p => p.TotalPoints) + this.Captain.TotalPoints;
        public Player Captain => this.Players.MaxBy(p => p.TotalPoints).First();

        public StartingEleven(IReadOnlyList<Player> players, Formation formation)
        {
            this.Players = players;
            this.Formation = formation;
        }
    }
}