namespace Fpl
{
    using System.Collections.Generic;
    using System.Linq;

    public class StartingEleven
    {
        public IReadOnlyList<Player> Players { get; }
        public Formation Formation { get; }
        public int TotalPoints => this.Players.Sum(p => p.TotalPoints) + this.Players.Max(p => p.TotalPoints);

        public StartingEleven(IReadOnlyList<Player> players, Formation formation)
        {
            this.Players = players;
            this.Formation = formation;
        }
    }
}