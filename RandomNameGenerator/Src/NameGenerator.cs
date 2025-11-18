using System.Reflection;

namespace BenScr.Random
{
    public sealed class NameGenerator
    {
        public static string[] firstNames;
        public static string[] lastNames;

        private static readonly System.Random random = new System.Random();

        public NameGenerator()
        {
            if (firstNames == null)
            {
                var assembly = Assembly.GetExecutingAssembly();
                firstNames = ReadResourceLines(assembly, "RandomNameGenerator.Resources.FirstNames.txt");
                lastNames = ReadResourceLines(assembly, "RandomNameGenerator.Resources.LastNames.txt");
            }
        }

        private static string[] ReadResourceLines(Assembly assembly, string resourceName)
        {
            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public string FullName(int names = 1)
        {
            string fullName = "";

            for (int i = 0; i < names; i++)
                fullName += FirstName() + " ";

            fullName += LastName();
            return fullName;
        }

        public string FirstName() => Capitalize(firstNames[random.Next(firstNames.Length)]);
        public string LastName() => Capitalize(lastNames[random.Next(lastNames.Length)]);

        private static string Capitalize(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }

        public int FirstNamesCount() => firstNames.Length;
        public int LastNamesCount() => lastNames.Length;
    }
}
