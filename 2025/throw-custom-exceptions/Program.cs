var withdrawalAmount = 100;
var account = new { Balance = 50 };

if (withdrawalAmount > account.Balance)
{
    throw new InsufficientFundsException(account.Balance, withdrawalAmount);
}

public class InsufficientFundsException : Exception
{
    public decimal Balance { get; }
    public decimal WithdrawalAmount { get; }

    public InsufficientFundsException(decimal balance, decimal withdrawalAmount)
        : base($"Attempted to withdraw {withdrawalAmount:C}, but only {balance:C} available.")
    {
        Balance = balance;
        WithdrawalAmount = withdrawalAmount;
    }
}


public class InsufficientFundsExceptionAlt(decimal balance, decimal withdrawalAmount)
     : Exception($"Attempted to withdraw {withdrawalAmount:C}, but only {balance:C} available.")
{
    public decimal Balance { get; } = balance;
    public decimal WithdrawalAmount { get; } = withdrawalAmount;
}
