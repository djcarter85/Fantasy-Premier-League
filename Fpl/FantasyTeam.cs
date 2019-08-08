namespace Fpl
{
    using System.Collections.Generic;
    using System.Linq;

    public class FantasyTeam
    {
        public IReadOnlyList<Player> Players { get; }

        public static readonly IReadOnlyDictionary<Position, int> PositionCounts = new Dictionary<Position, int>()
        {
            {Position.Goalkeeper, 2},
            {Position.Defender, 5},
            {Position.Midfielder, 5},
            {Position.Forward, 3},
        };

        public FantasyTeam(IReadOnlyList<Player> players)
        {
            this.Players = players;
        }

        public bool IsValid()
        {
            if (this.TotalPrice > 1000)
            {
                return false;
            }

            if (this.Players.GroupBy(p => p.TeamId).Any(g => g.Count() > 3))
            {
                return false;
            }

            if (this.Players.Distinct().Count() != this.Players.Count)
            {
                return false;
            }

            var playersByPosition = this.Players.ToLookup(p => p.Position);

            foreach (var positionCount in PositionCounts)
            {
                if (playersByPosition[positionCount.Key].Count() != positionCount.Value)
                {
                    return false;
                }
            }

            return true;
        }

        public int TotalPrice => this.Players.Sum(p => p.Price);

        public int TotalPoints => this.Players.Sum(p => p.TotalPoints);
    }
}