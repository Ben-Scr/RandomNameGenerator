# Random Name Generator
A C# library for generating random names

## How to use
```csharp
using BenScr.RandomNameGenerator;
```

```csharp
NameGenerator nameGenerator = new NameGenerator();
Console.WriteLine(nameGenerator.FirstName());
Console.WriteLine(nameGenerator.LastName());
Console.WriteLine(nameGenerator.FullName(2));
```
