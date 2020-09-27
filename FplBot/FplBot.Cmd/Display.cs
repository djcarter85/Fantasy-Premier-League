namespace FplBot.Cmd
{
    using System;

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
                Cmd.Season.Season1617 => "2016-17",
                Cmd.Season.Season1718 => "2017-18",
                Cmd.Season.Season1819 => "2018-19",
                Cmd.Season.Season1920 => "2019-20",
                _ => throw new ArgumentOutOfRangeException(nameof(season), season, null)
            };
        }
    }
}