using System;

class Program
{
    static void Main()
    {
        CreditCard card = new CreditCard("6989 3979 4969 5959", "Raphinha", new DateTime(2026, 12, 31), 9999, 900, 10000);

        card.OnDeposit += LogToConsole;
        card.OnWithdraw += LogToConsole;
        card.OnCreditUsageStart += LogToConsole;
        card.OnTargetBalanceReached += LogToConsole;
        card.OnPinChanged += LogToConsole;

        card.ShowCardInfo();

        Console.WriteLine("\nПопытка внести $200 на счет...");
        card.Deposit(200);

        Console.WriteLine("\nПопытка снять $50...");
        card.Withdraw(50);

        Console.WriteLine("\nПопытка снять $300...");
        card.Withdraw(300);

        Console.WriteLine("\nПроверка, достигнут ли целевой баланс $500...");
        card.CheckTargetBalance(500);

        Console.WriteLine("\nСмена PIN-кода на 5678...");
        card.ChangePin(5678);

        card.ShowCardInfo();
    }

    static void LogToConsole(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}
