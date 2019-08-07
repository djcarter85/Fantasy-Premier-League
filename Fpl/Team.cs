namespace Fpl
{
    public class Team
    {
        public int Id { get; }
        public string ShortName { get; }

        public Team(int id, string shortName)
        {
            this.Id = id;
            this.ShortName = shortName;
        }
    }
}