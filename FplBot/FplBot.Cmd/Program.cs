namespace FplBot.Cmd
{
    using System;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var season = Season.Season1920;

            var teams = new TeamRepository().GetAllTeams(season);
            var fixtures = new FixtureRepository().GetAllFixtures(season);

            foreach (var fixture in fixtures)
            {
                var homeTeam = teams[fixture.HomeTeamId];
                var awayTeam = teams[fixture.AwayTeamId];

                Console.WriteLine($"{homeTeam.ShortName} {fixture.FinalScore.Home}-{fixture.FinalScore.Away} {awayTeam.ShortName}");
            }
        }
    }
}
