namespace RandomNameGenerator
{
    public class NameGenerator
    {
        public static string[] firstNames;
        public static string[] lastNames;

        private static readonly Random random = new Random();

        public NameGenerator()
        {
            if (firstNames == null)
            {
                firstNames = File.ReadAllLines("Resources/FirstNames.txt");
                lastNames = File.ReadAllLines("Resources/LastNames.txt");
            }
        }

        public string FullName(int names = 1)
        {
            string fullName = "";

            for (int i = 0; i < names; i++)
                fullName += FirstName() + " ";

            fullName += LastName();
            return fullName;
        }

        public string FirstName() => firstNames[random.Next(firstNames.Length)];
        public string LastName() => lastNames[random.Next(lastNames.Length)];

        public int FirstNamesCount() => firstNames.Length;
        public int LastNamesCount() => lastNames.Length;
    }
}
