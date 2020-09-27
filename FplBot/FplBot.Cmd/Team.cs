namespace FplBot.Cmd
{
    public class Team
    {
        public Team(int id, string name, string shortName)
        {
            this.Id = id;
            this.Name = name;
            this.ShortName = shortName;
        }

        public int Id { get; }

        public string Name { get; }

        public string ShortName { get; }
    }
}