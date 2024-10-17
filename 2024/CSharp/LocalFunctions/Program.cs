using System;
using System.Collections.Generic;
using System.Linq;

var processor = new Examples();

var report = processor.GenerateSalesReport();
Console.WriteLine($"Sales Report: Total Sales = {report.Total}");

var receipt = processor.GenerateReceipt();
Console.WriteLine($"Receipt: Total = {receipt.Total}");

var inventorySummary = processor.GenerateInventorySummary();
Console.WriteLine($"Inventory Summary: Total Value = {inventorySummary.TotalValue}");

var payroll = processor.GeneratePayroll();
Console.WriteLine($"Payroll: Total Payroll = {payroll.TotalPayroll}");

var budgetPlan = processor.GenerateBudgetPlan();
Console.WriteLine($"Budget Plan: Total Budget = {budgetPlan.TotalBudget}");

public class Examples
{
    public Report GenerateSalesReport()
    {
        var salesData = GetMonthlySales();
        var totalSales = CalculateTotalSales();

        decimal CalculateTotalSales()
        {
            return salesData.Sum(sale => sale.Amount);
        }

        return new Report(salesData, totalSales);
    }

    public Receipt GenerateReceipt()
    {
        var items = GetPurchasedItems();
        var total = CalculateTotal(0.07M);

        decimal CalculateTotal(decimal taxRate)
        {
            return items.Sum(item => item.Price * item.Quantity) * (1 + taxRate);
        }

        return new Receipt(items, total);
    }

    public InventorySummary GenerateInventorySummary()
    {
        var products = GetInventory();
        var totalValue = CalculateTotalValue(products);

        static decimal CalculateTotalValue(IEnumerable<Product> products)
        {
            return products.Sum(product => product.Quantity * product.UnitPrice);
        }

        return new InventorySummary(products, totalValue);
    }

    public Payroll GeneratePayroll()
    {
        var bonusRate = 0.05M;
        var employees = GetEmployees();
        var totalPayroll = CalculateTotalPayroll(employees, bonusRate);

        static decimal CalculateTotalPayroll(IEnumerable<Employee> employees, decimal bonusRate)
        {
            return employees.Sum(employee => employee.Salary * (1 + bonusRate));
        }

        return new Payroll(employees, totalPayroll);
    }

    public BudgetPlan GenerateBudgetPlan()
    {
        var discount = 0.10M;
        var expenses = GetExpenses();
        var totalBudget = CalculateTotalBudget(500);

        decimal CalculateTotalBudget(decimal bufferAmount)
        {
            return (expenses.Sum(expense => expense.Cost) * (1 - discount)) + bufferAmount;
        }

        return new BudgetPlan(expenses, totalBudget);
    }

    // Mock data and classes
    private List<Sale> GetMonthlySales() => new() { new Sale(100), new Sale(200), new Sale(300) };
    private List<Item> GetPurchasedItems() => new() { new Item(10, 2), new Item(15, 3) };
    private List<Product> GetInventory() => new() { new Product(5, 20), new Product(10, 15) };
    private List<Employee> GetEmployees() => new() { new Employee(3000), new Employee(3500) };
    private List<Expense> GetExpenses() => new() { new Expense(100), new Expense(200), new Expense(150) };

    public record Sale(decimal Amount);
    public record Item(decimal Price, int Quantity);
    public record Product(int Quantity, decimal UnitPrice);
    public record Employee(decimal Salary);
    public record Expense(decimal Cost);
    public record Report(IEnumerable<Sale> Sales, decimal Total);
    public record Receipt(IEnumerable<Item> Items, decimal Total);
    public record InventorySummary(IEnumerable<Product> Products, decimal TotalValue);
    public record Payroll(IEnumerable<Employee> Employees, decimal TotalPayroll);
    public record BudgetPlan(IEnumerable<Expense> Expenses, decimal TotalBudget);
}
