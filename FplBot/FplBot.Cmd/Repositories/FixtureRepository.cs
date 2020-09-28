namespace FplBot.Cmd.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using CsvHelper;
    using CsvHelper.Configuration.Attributes;
    using FplBot.Cmd.Model;

    public static class FixtureRepository
    {
        public static IReadOnlyList<CompletedFixture> GetAllCompletedFixtures(Season season)
        {
            using (var reader = new StreamReader(GetFilePath(season)))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<CsvFixture>();

                return records
                    .Where(IsCompleted)
                    .Select(ConstructCompletedFixture)
                    .ToList();
            }
        }

        public static IReadOnlyList<Fixture> GetAllFixtures(Season season, int gameweekNumber)
        {
            using (var reader = new StreamReader(GetFilePath(season)))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<CsvFixture>();

                return records
                    .Where(cf => IsInGameweek(cf, gameweekNumber))
                    .Select(ConstructFixture)
                    .ToList();
            }
        }

        private static bool IsCompleted(CsvFixture csvFixture)
        {
            return !string.IsNullOrEmpty(csvFixture.TeamHScore) && !string.IsNullOrEmpty(csvFixture.TeamAScore);
        }

        private static bool IsInGameweek(CsvFixture csvFixture, int gameweekNumber)
        {
            return csvFixture.Event == gameweekNumber.ToString("F1");
        }

        private static CompletedFixture ConstructCompletedFixture(CsvFixture csvFixture)
        {
            return new CompletedFixture(
                csvFixture.TeamH,
                csvFixture.TeamA,
                csvFixture.TeamHDifficulty,
                csvFixture.TeamADifficulty,
                new Score((int)Convert.ToDouble(csvFixture.TeamHScore), (int)Convert.ToDouble(csvFixture.TeamAScore)));
        }

        private static Fixture ConstructFixture(CsvFixture csvFixture)
        {
            return new Fixture(
                csvFixture.TeamH,
                csvFixture.TeamA,
                csvFixture.TeamHDifficulty,
                csvFixture.TeamADifficulty);
        }

        private static string GetFilePath(Season season)
        {
            return Path.Combine(CsvDirectory.GetDirectoryPath(season), "fixtures.csv");
        }

        private class CsvFixture
        {
            [Name("team_h")]
            public int TeamH { get; set; }

            [Name("team_a")]
            public int TeamA { get; set; }

            [Name("team_h_difficulty")]
            public int TeamHDifficulty { get; set; }

            [Name("team_a_difficulty")]
            public int TeamADifficulty { get; set; }

            [Name("team_h_score")]
            public string TeamHScore { get; set; }

            [Name("team_a_score")]
            public string TeamAScore { get; set; }

            [Name("event")]
            public string Event { get; set; }
        }
    }
}