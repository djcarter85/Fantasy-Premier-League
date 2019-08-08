namespace Fpl
{
    using System;

    public static class ExtensionMethods
    {
        public static string ShortName(this Position position)
        {
            switch (position)
            {
                case Position.Goalkeeper:
                    return "GKP";
                case Position.Defender:
                    return "DEF";
                case Position.Midfielder:
                    return "MID";
                case Position.Forward:
                    return "FWD";
                default:
                    throw new ArgumentOutOfRangeException(nameof(position), position, null);
            }
        }
    }
}