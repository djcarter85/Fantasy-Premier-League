namespace Fpl
{
    using CsvHelper.Configuration.Attributes;

    public class CsvTeam
    {
        [Name("id")]
        public int Id { get; set; }

        [Name("short_name")]
        public string ShortName { get; set; }

        [Name("name")]
        public string Name { get; set; }
    }
}