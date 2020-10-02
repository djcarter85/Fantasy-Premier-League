namespace FplPtoBot.Cmd.Calculations
{
    using FplPtoBot.Cmd.Model;

    public static class PointsCalculator
    {
        public static int CalculatePoints(Score predicted, Score actual)
        {
            if (predicted.GetMatchResult() == actual.GetMatchResult())
            {
                if (predicted.Home == actual.Home && predicted.Away == actual.Away)
                {
                    return 5;
                }

                return 3;
            }
            else
            {
                if (predicted.Home == actual.Home || predicted.Away == actual.Away)
                {
                    return 1;
                }

                return 0;
            }
        }
    }
}