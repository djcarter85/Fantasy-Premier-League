namespace Fpl
{
    using CsvHelper.Configuration.Attributes;

    public class CsvFixture
    {
        [Name("event")]
        public int Event { get; set; }

        [Name("team_h")]
        public int TeamHomeId { get; set; }

        [Name("team_a")]
        public int TeamAwayId { get; set; }

        [Name("team_h_difficulty")]
        public int TeamHomeDifficulty { get; set; }

        [Name("team_a_difficulty")]
        public int TeamAwayDifficulty { get; set; }

    }
}