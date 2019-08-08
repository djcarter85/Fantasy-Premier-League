namespace Fpl
{
    using System.Collections.Generic;

    public class Formation
    {
        private Formation(int defenders, int midfielders, int forwards)
        {
            this.PositionCounts = new Dictionary<Position, int>
            {
                {Position.Goalkeeper, 1},
                {Position.Defender, defenders},
                {Position.Midfielder, midfielders},
                {Position.Forward, forwards},
            };
        }

        public IReadOnlyDictionary<Position, int> PositionCounts { get; }

        public static readonly IReadOnlyList<Formation> AllValid = new[]
        {
            new Formation(5, 4, 1),
            new Formation(5, 3, 2),
            new Formation(5, 2, 3),
            new Formation(4, 5, 1),
            new Formation(4, 4, 2),
            new Formation(4, 3, 3),
            new Formation(3, 5, 2),
            new Formation(3, 4, 3),
        };

        public override string ToString()
        {
            return
                $"{this.PositionCounts[Position.Defender]}-{this.PositionCounts[Position.Midfielder]}-{this.PositionCounts[Position.Forward]}";
        }
    }
}