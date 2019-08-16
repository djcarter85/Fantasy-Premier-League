namespace Fpl
{
    using System.Collections.Generic;
    using System.Linq;

    public class TeamSchedule
    {
        public int TeamId { get; }
        public IReadOnlyList<DirectionalFixture> DirectionalFixtures { get; }

        public TeamSchedule(int teamId, IReadOnlyList<DirectionalFixture> directionalFixtures)
        {
            this.TeamId = teamId;
            this.DirectionalFixtures = directionalFixtures;
        }

        public int TotalDifficulty(GameweekInterval interval) =>
            DirectionalFixtures
                .Where(df => interval.Contains(df.Gameweek))
                .Sum(df => df.Difficulty);
    }
}