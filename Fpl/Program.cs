namespace Fpl
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using CsvHelper;
    using OfficeOpenXml;

    public static class Program
    {
        public static void Main()
        {
            var players = FetchPlayers();
            var teams = FetchTeams();
            OutputPlayers(players, teams);
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
                    .Select(ct => new Team(ct.Id, ct.ShortName))
                    .ToDictionary(t => t.Id);
            }
        }

        private static void OutputPlayers(IReadOnlyList<Player> players, IReadOnlyDictionary<int, Team> teams)
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

                worksheet.Cells[rowIndex, 1].Value = "Full Name";
                worksheet.Cells[rowIndex, 2].Value = "Position";
                worksheet.Cells[rowIndex, 3].Value = "Team";
                worksheet.Cells[rowIndex, 4].Value = "Price";
                worksheet.Cells[rowIndex, 5].Value = "Total Points";
                worksheet.Cells[rowIndex, 6].Value = "Return On Investment";
                worksheet.Cells[rowIndex, 7].Value = "Minutes Played";

                rowIndex++;

                foreach (var player in players)
                {
                    worksheet.Cells[rowIndex, 1].Value = player.FullName;
                    worksheet.Cells[rowIndex, 2].Value = player.Position;
                    worksheet.Cells[rowIndex, 3].Value = teams[player.TeamId].ShortName;
                    worksheet.Cells[rowIndex, 4].Value = player.Price;
                    worksheet.Cells[rowIndex, 5].Value = player.TotalPoints;
                    worksheet.Cells[rowIndex, 6].Value = player.ReturnOnInvestment(0);
                    worksheet.Cells[rowIndex, 7].Value = player.Minutes;

                    rowIndex++;
                }


                package.SaveAs(new FileInfo(@"C:\a\FPL\players.xlsx"));
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
