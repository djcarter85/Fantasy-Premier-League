namespace Fpl
{
    using CsvHelper.Configuration.Attributes;

    public class CsvPlayer
    {
        [Name("first_name")]
        public string FirstName { get; set; }

        [Name("second_name")]
        public string SecondName { get; set; }

        [Name("total_points")]
        public int TotalPoints { get; set; }

        [Name("now_cost")]
        public int NowCost { get; set; }

        [Name("minutes")]
        public int Minutes { get; set; }

        [Name("team")]
        public int Team { get; set; }

        [Name("element_type")]
        public int ElementType { get; set; }
    }
}