namespace Strategy;

public class PaymentService(IPaymentStrategy paymentStrategy)
{
    private readonly IPaymentStrategy _paymentStrategy = paymentStrategy;

    public void Pay(double amount)
    {
        Console.WriteLine("PaymentService Pre-logic");
        _paymentStrategy.ProcessPayment(amount);
        Console.WriteLine("PaymentService Post-logic");
    }
}
