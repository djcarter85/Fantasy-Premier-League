namespace Fpl
{
    using System.Collections.Generic;
    using System.Linq;
    using MoreLinq;

    public static class TeamSelector
    {
        public static FantasyTeam SelectRandomTeam(IReadOnlyList<Player> players)
        {
            var playersByPosition = players.ToLookup(p => p.Position);

            var selectedPlayers = new List<Player>();

            foreach (var positionCount in FantasyTeam.PositionCounts)
            {
                selectedPlayers.AddRange(playersByPosition[positionCount.Key].RandomSubset(positionCount.Value));
            }

            return new FantasyTeam(selectedPlayers);
        }
    }
}