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
            OutputPlayers(players);
        }

        private static void OutputPlayers(IReadOnlyList<Player> players)
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

                worksheet.Cells[rowIndex, 1].Value = "Full Name";
                worksheet.Cells[rowIndex, 2].Value = "Position";
                worksheet.Cells[rowIndex, 3].Value = "Team Id";
                worksheet.Cells[rowIndex, 4].Value = "Price";
                worksheet.Cells[rowIndex, 5].Value = "Total Points";
                worksheet.Cells[rowIndex, 6].Value = "Return On Investment";

                rowIndex++;

                foreach (var player in players)
                {
                    worksheet.Cells[rowIndex, 1].Value = player.FullName;
                    worksheet.Cells[rowIndex, 2].Value = player.Position;
                    worksheet.Cells[rowIndex, 3].Value = player.TeamId;
                    worksheet.Cells[rowIndex, 4].Value = player.Price;
                    worksheet.Cells[rowIndex, 5].Value = player.TotalPoints;
                    worksheet.Cells[rowIndex, 6].Value = player.ReturnOnInvestment(0);

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
                        GetPosition(cp.ElementType)))
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
