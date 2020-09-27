namespace FplBot.Cmd
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using CsvHelper;

    public class FixtureRepository
    {
        public IReadOnlyList<Fixture> GetAllFixtures(Season season)
        {
            using (var reader = new StreamReader(GetFilePath(season)))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<CsvFixture>();

                return records
                    .Select(ConstructFixture)
                    .ToList();
            }
        }

        private static Fixture ConstructFixture(CsvFixture csvFixture)
        {
            return new Fixture(
                csvFixture.TeamH,
                csvFixture.TeamA,
                csvFixture.TeamHDifficulty,
                csvFixture.TeamADifficulty,
                new Score(csvFixture.TeamHScore, csvFixture.TeamAScore));
        }

        private static string GetFilePath(Season season)
        {
            return Path.Combine(GetDirectoryPath(season), "fixtures.csv");
        }

        private static string GetDirectoryPath(Season season)
        {
            var repositoryRoot = @"C:\git\Fantasy-Premier-League";
            return Path.Combine(repositoryRoot, "data", GetDirectoryName(season));
        }

        private static string GetDirectoryName(Season season)
        {
            return season switch
            {
                Season.Season1920 => "2019-20",
                _ => throw new ArgumentOutOfRangeException(nameof(season), season, null)
            };
        }
    }
}