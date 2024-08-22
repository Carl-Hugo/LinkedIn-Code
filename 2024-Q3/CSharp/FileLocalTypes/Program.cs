#nullable enable
using Utilities;

Console.WriteLine("Enter your name:");
var name = Console.ReadLine();
var greeter = new Greeter();
var length = name.Length;
greeter.Greet(name);