using System.Reflection;
using System.Text;

namespace BenScr.RandomNameGenerator
{
    public sealed class NameGenerator
    {
        private static readonly string[] firstNames =
            ReadResourceLines(typeof(NameGenerator).Assembly, "RandomNameGenerator.Resources.FirstNames.txt");

        private static readonly string[] lastNames =
            ReadResourceLines(typeof(NameGenerator).Assembly, "RandomNameGenerator.Resources.LastNames.txt");

        /// <summary>Creates a new <see cref="NameGenerator"/>.</summary>
        public NameGenerator()
        {
        }

        private static string[] ReadResourceLines(Assembly assembly, string resourceName)
        {
            using var stream = assembly.GetManifestResourceStream(resourceName)
                ?? throw new InvalidOperationException(
                    $"Embedded resource '{resourceName}' was not found. " +
                    $"Available resources: {string.Join(", ", assembly.GetManifestResourceNames())}");
            using var reader = new StreamReader(stream);

            var tokens = reader.ReadToEnd().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var names = new List<string>(tokens.Length);
            foreach (var token in tokens)
            {
                if (IsValidName(token))
                    names.Add(token);
            }

            if (names.Count == 0)
                throw new InvalidOperationException($"Embedded resource '{resourceName}' contained no usable names.");

            return names.ToArray();
        }

        private static bool IsValidName(string token)
        {
            foreach (char c in token)
            {
                if (!char.IsLetter(c) && c != '\'' && c != '-')
                    return false;
            }

            return true;
        }

        public string FullName(int names = 1)
        {
            if (names < 1)
                throw new ArgumentOutOfRangeException(nameof(names), names, "At least one first name is required.");

            var sb = new StringBuilder();
            for (int i = 0; i < names; i++)
                sb.Append(FirstName()).Append(' ');

            sb.Append(LastName());
            return sb.ToString();
        }

        /// <summary>Returns a random, title-cased first name.</summary>
        public string FirstName() => Capitalize(firstNames[Random.Shared.Next(firstNames.Length)]);

        /// <summary>Returns a random, title-cased last name.</summary>
        public string LastName() => Capitalize(lastNames[Random.Shared.Next(lastNames.Length)]);

        private static string Capitalize(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToUpperInvariant(input[0]) + input.Substring(1).ToLowerInvariant();
        }

        /// <summary>Returns the number of first names within the array.</summary>
        public static int FirstNamesCount() => firstNames.Length;

        /// <summary>Returns the number of last names within the array.</summary>
        public static int LastNamesCount() => lastNames.Length;

        /// <summary>Returns a copy of all first names in the array.</summary>
        public static string[] GetFirstNames() => (string[])firstNames.Clone();

        /// <summary>Returns a copy of all last names in the array.</summary>
        public static string[] GetLastNames() => (string[])lastNames.Clone();
    }
}
