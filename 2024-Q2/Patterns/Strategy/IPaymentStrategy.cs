namespace Strategy;

public interface IPaymentStrategy
{
    void ProcessPayment(double amount);
}

public class CreditCardPayment : IPaymentStrategy
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Processing ${amount} via Credit Card.");
    }
}

public class PayPalPayment : IPaymentStrategy
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Processing ${amount} via PayPal.");
    }
}

public class CryptoPayment : IPaymentStrategy
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Processing ${amount} via Cryptocurrency.");
    }
}
