namespace Fpl
{
    public class Player
    {
        public Player(string firstName, string secondName, int totalPoints, int price, int teamId, Position position)
        {
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.TotalPoints = totalPoints;
            this.Price = price;
            this.TeamId = teamId;
            this.Position = position;

            this.FullName = $"{this.FirstName} {this.SecondName}";
        }

        public string FirstName { get; }
        public string SecondName { get; }
        public int TotalPoints { get; }
        public int Price { get; }
        public int TeamId { get; }
        public Position Position { get; }

        public string FullName { get; }
        public decimal ReturnOnInvestment(int baselinePrice) => (decimal)this.TotalPoints / (this.Price - baselinePrice);
    }
}