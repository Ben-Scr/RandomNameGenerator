using BenScr.Math.Random;

public static class Program
{
    public static void Main(string[] args)
    {
        NameGenerator nameGenerator = new NameGenerator();
        Console.WriteLine(nameGenerator.FirstName());
        Console.WriteLine(nameGenerator.LastName());
        Console.WriteLine(nameGenerator.FullName(2));
    }
}