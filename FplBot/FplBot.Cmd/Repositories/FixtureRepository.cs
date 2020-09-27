namespace FplBot.Cmd.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using CsvHelper;
    using FplBot.Cmd.Model;

    public class FixtureRepository
    {
        public IReadOnlyList<Fixture> GetAllFixtures(Season season)
        {
            using (var reader = new StreamReader(GetFilePath(season)))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<CsvFixture>();

                return records
                    .Where(IsFinished)
                    .Select(ConstructFixture)
                    .ToList();
            }
        }

        private static bool IsFinished(CsvFixture csvFixture)
        {
            return !string.IsNullOrEmpty(csvFixture.TeamHScore) && !string.IsNullOrEmpty(csvFixture.TeamAScore);
        }

        private static Fixture ConstructFixture(CsvFixture csvFixture)
        {
            return new Fixture(
                csvFixture.TeamH,
                csvFixture.TeamA,
                csvFixture.TeamHDifficulty,
                csvFixture.TeamADifficulty,
                new Score((int)Convert.ToDouble(csvFixture.TeamHScore), (int)Convert.ToDouble(csvFixture.TeamAScore)));
        }

        private static string GetFilePath(Season season)
        {
            return Path.Combine(CsvDirectory.GetDirectoryPath(season), "fixtures.csv");
        }
    }
}