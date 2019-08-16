namespace Fpl
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using CommandLine;
    using CsvHelper;
    using OfficeOpenXml;

    public static class Program
    {
        [Verb("select")]
        private class SelectOptions
        {
        }

        [Verb("proc")]
        private class ProcessOptions
        {
            [Value(0, Required = true)]
            public string FilePath { get; set; }
        }

        [Verb("fdr")]
        private class FdrOptions
        {
            [Value(0, Required = true)]
            public int NextGameweek { get; set; }
        }

        public static int Main(string[] args)
        {
            return CommandLine.Parser.Default.ParseArguments<SelectOptions, ProcessOptions, FdrOptions>(args)
                .MapResult(
                    (SelectOptions so) =>
                    {
                        var allPlayers = FetchPlayers();
                        var teams = FetchTeams();

                        var fantasyTeam = TeamSelector.SelectRandomTeam(allPlayers);
                        DisplayFantasyTeamAndStartingEleven(fantasyTeam, teams);

                        while (Console.ReadLine() == string.Empty)
                        {
                            for (int i = 0; i < 10000; i++)
                            {
                                fantasyTeam = TeamSelector.ImproveTeam(fantasyTeam, allPlayers);
                            }

                            DisplayFantasyTeamAndStartingEleven(fantasyTeam, teams);
                        }

                        return 0;
                    },
                    (ProcessOptions po) =>
                    {
                        var allPlayers = FetchPlayers();
                        var teams = FetchTeams();

                        OutputPlayers(allPlayers, teams, po.FilePath);

                        Console.WriteLine($"Player info output to '{po.FilePath}'");

                        return 0;
                    },
                    (FdrOptions fo) =>
                    {
                        var interval = new GameweekInterval(fo.NextGameweek, 5);

                        var teams = FetchTeams();

                        var fixtures = FetchFixtures();

                        var directionalFixtures = FetchDirectionalFixtures(fixtures);

                        var teamSchedules = TeamSchedules(directionalFixtures);

                        DisplayTeamSchedules(teamSchedules, teams, interval);

                        return 0;
                    },
                    e => 1);
        }

        private static IReadOnlyList<TeamSchedule> TeamSchedules(IReadOnlyList<DirectionalFixture> directionalFixtures)
        {
            return directionalFixtures
                .GroupBy(df => df.TeamId)
                .Select(g => new TeamSchedule(g.Key, g.ToList()))
                .ToList();
        }

        private static void DisplayTeamSchedules(
            IReadOnlyList<TeamSchedule> teamSchedules,
            IReadOnlyDictionary<int, Team> teams,
            GameweekInterval interval)
        {
            foreach (var teamSchedule in teamSchedules.OrderBy(ts => ts.TotalDifficulty(interval)))
            {
                var team = teams[teamSchedule.TeamId];

                var upcomingFixtures = teamSchedule.DirectionalFixtures
                    .Where(df => interval.Contains(df.Gameweek))
                    .Select(df =>
                    {
                        var opponent = teams[df.OpponentId];
                        var homeAway = df.Location == MatchLocation.Home ? "H" : "A";
                        var x = new string('|', df.Difficulty) + new string('.', 5 - df.Difficulty);
                        return $"{opponent.ShortName} ({homeAway}) {x}";
                    });

                Console.WriteLine($"{team.Name,-15} {teamSchedule.TotalDifficulty(interval),3}   {string.Join("   ", upcomingFixtures)}");
            }
        }

        private static IReadOnlyList<DirectionalFixture> FetchDirectionalFixtures(IReadOnlyList<Fixture> fixtures)
        {
            var directionalFixtures = new List<DirectionalFixture>();

            foreach (var fixture in fixtures)
            {
                directionalFixtures.Add(
                    new DirectionalFixture(
                        fixture.Gameweek,
                        fixture.HomeTeamId,
                        fixture.AwayTeamId,
                        MatchLocation.Home,
                        fixture.HomeTeamDifficulty));
                directionalFixtures.Add(
                    new DirectionalFixture(
                        fixture.Gameweek,
                        fixture.AwayTeamId,
                        fixture.HomeTeamId,
                        MatchLocation.Away,
                        fixture.AwayTeamDifficulty));
            }

            return directionalFixtures;
        }

        private static void DisplayFantasyTeamAndStartingEleven(FantasyTeam fantasyTeam, IReadOnlyDictionary<int, Team> teams)
        {
            var bestStartingEleven = fantasyTeam.BestStartingEleven();

            foreach (var position in new[] { Position.Goalkeeper, Position.Defender, Position.Midfielder, Position.Forward })
            {
                foreach (var player in fantasyTeam.Players.Where(p => p.Position == position).OrderByDescending(p => p.TotalPoints))
                {
                    Console.WriteLine(
                        $"{CaptainMarker(bestStartingEleven, player)} {player.Position.ShortName()}  {player.FullName,-35} {teams[player.TeamId].ShortName}  {player.TotalPoints,3}  {FormatPrice(player.Price)}");
                }

                Console.WriteLine();
            }

            Console.WriteLine($"{bestStartingEleven.Formation}, {bestStartingEleven.TotalPoints} pts, {FormatPrice(fantasyTeam.TotalPrice)}");
            Console.WriteLine();
        }

        private static string CaptainMarker(StartingEleven bestStartingEleven, Player player)
        {
            if (bestStartingEleven.Players.Contains(player))
            {
                if (player == bestStartingEleven.Captain)
                {
                    return "**";
                }
                else
                {
                    return " *";
                }
            }
            else
            {
                return "  ";
            }
        }

        private static string FormatPrice(int price)
        {
            return $"£{(decimal)price / 10:N1}m";
        }

        private static IReadOnlyDictionary<int, Team> FetchTeams()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Fpl.teams.csv";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader))
            {
                var csvTeams = csv.GetRecords<CsvTeam>();

                return csvTeams
                    .Select(ct => new Team(ct.Id, ct.ShortName, ct.Name))
                    .ToDictionary(t => t.Id);
            }
        }

        private static IReadOnlyList<Fixture> FetchFixtures()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Fpl.fixtures.csv";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader))
            {
                var csvFixtures = csv.GetRecords<CsvFixture>();

                return csvFixtures
                    .Select(cf => new Fixture(cf.Event, cf.TeamHomeId, cf.TeamAwayId, cf.TeamHomeDifficulty, cf.TeamAwayDifficulty))
                    .ToList();
            }
        }

        private static void OutputPlayers(
            IReadOnlyList<Player> players,
            IReadOnlyDictionary<int, Team> teams,
            string filePath)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Players");

                var rowIndex = 1;

                worksheet.Column(1).Width = 40;
                worksheet.Column(2).Width = 15;
                worksheet.Column(3).Width = 10;
                worksheet.Column(4).Width = 15;
                worksheet.Column(5).Width = 15;
                worksheet.Column(6).Width = 20;
                worksheet.Column(7).Width = 15;
                worksheet.Column(8).Width = 20;

                worksheet.Cells[rowIndex, 1].Value = "Full Name";
                worksheet.Cells[rowIndex, 2].Value = "Position";
                worksheet.Cells[rowIndex, 3].Value = "Team";
                worksheet.Cells[rowIndex, 4].Value = "Price";
                worksheet.Cells[rowIndex, 5].Value = "Total Points";
                worksheet.Cells[rowIndex, 6].Value = "Return On Investment";
                worksheet.Cells[rowIndex, 7].Value = "Minutes Played";
                worksheet.Cells[rowIndex, 8].Value = "Points per minute";

                rowIndex++;

                foreach (var player in players)
                {
                    worksheet.Cells[rowIndex, 1].Value = player.FullName;
                    worksheet.Cells[rowIndex, 2].Value = player.Position;
                    worksheet.Cells[rowIndex, 3].Value = teams[player.TeamId].ShortName;
                    worksheet.Cells[rowIndex, 4].Value = player.Price;
                    worksheet.Cells[rowIndex, 5].Value = player.TotalPoints;
                    worksheet.Cells[rowIndex, 6].Value = player.ReturnOnInvestment;
                    worksheet.Cells[rowIndex, 7].Value = player.Minutes;
                    worksheet.Cells[rowIndex, 8].Value = player.PointsPerMinute;

                    rowIndex++;
                }

                package.SaveAs(new FileInfo(filePath));
            }
        }

        private static IReadOnlyList<Player> FetchPlayers()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Fpl.players_raw.csv";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader))
            {
                var csvPlayers = csv.GetRecords<CsvPlayer>();

                return csvPlayers
                    .Select(cp => new Player(cp.FirstName, cp.SecondName, cp.TotalPoints, cp.NowCost, cp.Team,
                        GetPosition(cp.ElementType), cp.Minutes))
                    .ToList();
            }
        }

        private static Position GetPosition(int elementType)
        {
            switch (elementType)
            {
                case 1: return Position.Goalkeeper;
                case 2: return Position.Defender;
                case 3: return Position.Midfielder;
                case 4: return Position.Forward;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
