namespace FplBot.Cmd.Repositories
{
    using System;
    using System.IO;
    using FplBot.Cmd.Model;

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
                Season.Season1617 => "2016-17",
                Season.Season1718 => "2017-18",
                Season.Season1819 => "2018-19",
                Season.Season1920 => "2019-20",
                Season.Season2021 => "2020-21",
                _ => throw new ArgumentOutOfRangeException(nameof(season), season, null)
            };
        }
    }
}