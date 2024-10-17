using System.Security.Cryptography;

// Using RandomNumberGenerator to shuffle and get a random item from a collection
var rngItems = new[] { "apple", "banana", "cherry", "date" }.AsSpan();
RandomNumberGenerator.Shuffle(rngItems);
var rngItem = RandomNumberGenerator.GetItems<string>(rngItems, 1).Single();
Console.WriteLine(rngItem);

// Using Random.Shared for thread-safe random number generation
var randomNumber = Random.Shared.Next();
Console.WriteLine(randomNumber);

// Using GetItems to get a random item from a collection
var items = new[] { "apple", "banana", "cherry", "date" };
var randomItem = Random.Shared.GetItems(items, 1).Single();
Console.WriteLine(randomItem);

// Using Shuffle to randomize a collection
var words = "Lorem ipsum dolor sit amet".Split(' ');
Random.Shared.Shuffle(words);
Console.WriteLine(string.Join(' ', words));
