namespace Fpl
{
    public class GameweekInterval
    {
        private readonly int first;
        private readonly int duration;

        public GameweekInterval(int first, int duration)
        {
            this.first = first;
            this.duration = duration;
        }

        public bool Contains(int gameweek) => gameweek >= this.first && gameweek < this.first + this.duration;
    }
}