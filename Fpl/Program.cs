namespace Fpl
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using CsvHelper;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var players = FetchPlayers();
            DisplayBestPlayers(players);
        }

        private static void DisplayBestPlayers(IReadOnlyList<Player> players)
        {
            foreach (var position in new[] { Position.Goalkeeper, Position.Defender, Position.Midfielder, Position.Forward })
            {
                Console.WriteLine(position.ToString().ToUpperInvariant());
                Console.WriteLine();

                var playersForThisPosition = players
                    .Where(p => p.Position == position)
                    .ToList();

                var highestTotalPlayers = playersForThisPosition
                    .OrderByDescending(p => p.TotalPoints)
                    .Take(5)
                    .ToList();

                Console.WriteLine("Highest total points");
                Console.WriteLine();

                foreach (var highestTotalPlayer in highestTotalPlayers)
                {
                    Console.WriteLine($"{highestTotalPlayer.FullName}: {highestTotalPlayer.TotalPoints} pts, {FormatPrice(highestTotalPlayer.Price)}");
                }

                Console.WriteLine();

                Console.WriteLine("Highest ROI");
                Console.WriteLine();

                var highestRoiPlayers = playersForThisPosition
                    .OrderByDescending(p => p.ReturnOnInvestment(baselinePrice: 0))
                    .Take(5)
                    .ToList();

                foreach (var highestRoiPlayer in highestRoiPlayers)
                {
                    Console.WriteLine($"{highestRoiPlayer.FullName}: {highestRoiPlayer.TotalPoints} pts, {FormatPrice(highestRoiPlayer.Price)}, {highestRoiPlayer.ReturnOnInvestment(baselinePrice: 0):N3}");
                }

                Console.WriteLine();

                Console.WriteLine("Highest ROI by baseline");
                Console.WriteLine();

                var baseline = playersForThisPosition.Min(p => p.Price) - 5;

                var highestRoiPlayersByBaseline = playersForThisPosition
                    .OrderByDescending(p => p.ReturnOnInvestment(baselinePrice: baseline))
                    .Take(5)
                    .ToList();

                foreach (var highestRoiPlayerByBaseline in highestRoiPlayersByBaseline)
                {
                    Console.WriteLine($"{highestRoiPlayerByBaseline.FullName}: {highestRoiPlayerByBaseline.TotalPoints} pts, {FormatPrice(highestRoiPlayerByBaseline.Price)}, {highestRoiPlayerByBaseline.ReturnOnInvestment(baselinePrice: baseline):N3}");
                }

                Console.WriteLine();
            }
        }

        private static string FormatPrice(int price)
        {
            return $"£{(decimal)price / 10}m";
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
