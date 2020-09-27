namespace FplBot.Cmd
{
    using System;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var fixtureRepository = new FixtureRepository();

            var fixtures = fixtureRepository.GetAllFixtures(Season.Season1920);

            Console.WriteLine(fixtures.Count);
        }
    }
}
