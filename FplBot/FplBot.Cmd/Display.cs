namespace FplBot.Cmd
{
    using System;
    using FplBot.Cmd.Model;

    public static class Display
    {
        public static string Score(Score score)
        {
            return $"{score.Home}-{score.Away}";
        }

        public static string Season(Season season)
        {
            return season switch
            {
                Model.Season.Season1617 => "2016-17",
                Model.Season.Season1718 => "2017-18",
                Model.Season.Season1819 => "2018-19",
                Model.Season.Season1920 => "2019-20",
                Model.Season.Season2021 => "2020-21",
                _ => throw new ArgumentOutOfRangeException(nameof(season), season, null)
            };
        }
    }
}