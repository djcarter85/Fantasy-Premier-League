namespace FplBot.Cmd.Repositories
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using CsvHelper;
    using CsvHelper.Configuration.Attributes;
    using FplBot.Cmd.Model;

    public static class TeamRepository
    {
        public static IReadOnlyDictionary<int, Team> GetAllTeams(Season season)
        {
            using (var reader = new StreamReader(GetFilePath(season)))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<CsvTeam>();

                return records
                    .Select(ConstructTeam)
                    .ToDictionary(t=>t.Id);
            }
        }

        private static Team ConstructTeam(CsvTeam csvTeam)
        {
            return new Team(
                csvTeam.Id,
                csvTeam.Name,
                csvTeam.ShortName);
        }

        private static string GetFilePath(Season season)
        {
            return Path.Combine(CsvDirectory.GetDirectoryPath(season), "teams.csv");
        }

        private class CsvTeam
        {
            [Name("id")]
            public int Id { get; set; }

            [Name("name")]
            public string Name { get; set; }

            [Name("short_name")]
            public string ShortName { get; set; }
        }
    }
}