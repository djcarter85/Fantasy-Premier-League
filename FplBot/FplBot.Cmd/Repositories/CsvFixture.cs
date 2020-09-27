namespace FplBot.Cmd.Repositories
{
    using CsvHelper.Configuration.Attributes;

    public class CsvFixture
    {
        [Name("team_h")]
        public int TeamH { get; set; }
        
        [Name("team_a")]
        public int TeamA { get; set; }
        
        [Name("team_h_difficulty")]
        public int TeamHDifficulty { get; set; }
        
        [Name("team_a_difficulty")]
        public int TeamADifficulty { get; set; }
        
        [Name("team_h_score")]
        public int TeamHScore { get; set; }
        
        [Name("team_a_score")]
        public int TeamAScore { get; set; }
    }
}