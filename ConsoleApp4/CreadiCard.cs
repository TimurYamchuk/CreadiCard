using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите номер карты: ");
        string cardNumber = Console.ReadLine();

        Console.Write("Введите имя владельца: ");
        string ownerName = Console.ReadLine();

        DateTime expirationDate;
        Console.Write("Введите дату истечения (ГГГГ-ММ-ДД): ");
        while (!DateTime.TryParse(Console.ReadLine(), out expirationDate) || expirationDate <= DateTime.Now)
            Console.Write("Неверная дата. Введите будущую дату (ГГГГ-ММ-ДД): ");

        Console.Write("Установите ПИН-код (4 цифры): ");
        int pin;
        while (!int.TryParse(Console.ReadLine(), out pin) || pin < 1000 || pin > 9999)
            Console.Write("ПИН-код должен быть 4-значным числом: ");

        Console.Write("Введите кредитный лимит: ");
        decimal creditLimit;
        while (!decimal.TryParse(Console.ReadLine(), out creditLimit) || creditLimit < 0)
            Console.Write("Лимит должен быть положительным числом: ");

        CreditCard card = new CreditCard(cardNumber, ownerName, expirationDate, pin, creditLimit, 0);
        card.OnDeposit += DisplayMessage;
        card.OnWithdraw += DisplayMessage;
        card.OnCreditUsage += DisplayMessage;
        card.OnBalanceGoalReached += DisplayMessage;
        card.OnPinUpdated += DisplayMessage;

        card.DisplayCardInfo();
        card.Deposit(200);
        card.Withdraw(50);
        card.Withdraw(300);
        card.CheckBalanceGoal(500);
        card.UpdatePin(5678);
        card.DisplayCardInfo();
    }

    static void DisplayMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}
