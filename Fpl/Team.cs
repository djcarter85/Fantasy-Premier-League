namespace Fpl
{
    public class Team
    {
        public int Id { get; }
        public string ShortName { get; }
        public string Name { get; }

        public Team(int id, string shortName, string name)
        {
            this.Id = id;
            this.ShortName = shortName;
            this.Name = name;
        }
    }
}