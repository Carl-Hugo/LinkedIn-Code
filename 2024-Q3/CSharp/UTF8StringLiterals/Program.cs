ReadOnlySpan<byte> welcomeMessage = "Welcome to the UTF-8 world!"u8;

Console.WriteLine("Message in UTF-8: ");
foreach (var b in welcomeMessage)
{
    Console.WriteLine($"{(char)b} ({b:X2})");
}
