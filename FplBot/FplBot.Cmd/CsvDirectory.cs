namespace FplBot.Cmd
{
    using System;
    using System.IO;

    public static class CsvDirectory
    {
        public static string GetDirectoryPath(Season season)
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