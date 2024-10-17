// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var rawString1 = """
    This is a raw string
    literal in C# 12.
    No escapes needed!
    """;

var rawString2 = """
    This is a raw string literal
    with "quotes" included.
    No escapes needed!
    """;

var name = "C# 12";
var rawString3 = $"""
    Welcome to {name}!
    Enjoy cleaner code.
    """;

var variable = 123;
var rawString4 = $"""
    This has a variable ({variable}) inside!
    """;

var age = 123;
var json = $$"""
    {
        "name": "John Doe",
        "age": {{age}}
    }
    """;


Console.WriteLine(rawString1);
Console.WriteLine("---");
Console.WriteLine(rawString2);
Console.WriteLine("---");
Console.WriteLine(rawString3);
Console.WriteLine("---");
Console.WriteLine(rawString4);
Console.WriteLine("---");
Console.WriteLine(json);
Console.WriteLine("---");