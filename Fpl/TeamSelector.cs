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

        public static FantasyTeam ImproveTeam(FantasyTeam fantasyTeam, IReadOnlyList<Player> allPlayers)
        {
            var players = fantasyTeam.Players.ToList();

            var playersToRemove = players.RandomSubset(2);

            foreach (var playerToRemove in playersToRemove)
            {
                players.Remove(playerToRemove);
            }

            var positions = playersToRemove.Select(p => p.Position);

            var potentialNewFantasyTeams = new List<FantasyTeam>
            {
                fantasyTeam
            };

            for (int i = 0; i < 10; i++)
            {
                var playersToAdd = positions.SelectMany(pos => allPlayers.Where(p => p.Position == pos).RandomSubset(1));

                var newFantasyTeam = new FantasyTeam(players.Concat(playersToAdd).ToList());

                if (newFantasyTeam.IsValid())
                {
                    potentialNewFantasyTeams.Add(newFantasyTeam);
                }
            }

            return potentialNewFantasyTeams.MaxBy(ft => ft.BestStartingEleven().TotalPoints).First();
        }
    }
}