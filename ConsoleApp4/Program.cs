using System;

public delegate void AccountEventHandler(string message);

public class CreditCard
{
    public string CardNumber { get; }
    public string OwnerName { get; }
    public DateTime ExpirationDate { get; }
    private int Pin { get; set; }
    public decimal CreditLimit { get; }
    public decimal Balance { get; private set; }

    public event AccountEventHandler OnDeposit;
    public event AccountEventHandler OnWithdraw;
    public event AccountEventHandler OnCreditUsage;
    public event AccountEventHandler OnBalanceGoalReached;
    public event AccountEventHandler OnPinUpdated;

    public CreditCard(string cardNumber, string ownerName, DateTime expirationDate, int pin, decimal creditLimit, decimal initialBalance = 0)
    {
        CardNumber = cardNumber;
        OwnerName = ownerName;
        ExpirationDate = expirationDate;
        Pin = pin;
        CreditLimit = creditLimit;
        Balance = initialBalance;
    }

    public void DisplayCardInfo()
    {
        Console.WriteLine($"\n--- Данные карты ---\nНомер: {CardNumber}\nВладелец: {OwnerName}\nСрок действия: {ExpirationDate.ToShortDateString()}\nБаланс: ${Balance}\nЛимит: ${CreditLimit}");
    }

    public void Deposit(decimal amount)
    {
        if (amount > 0)
        {
            Balance += amount;
            OnDeposit?.Invoke($"Баланс увеличен на ${amount}. Текущий баланс: ${Balance}");
        }
        else
        {
            Console.WriteLine("Введите сумму больше нуля.");
        }
    }

    public void Withdraw(decimal amount)
    {
        if (amount > 0 && amount <= Balance + CreditLimit)
        {
            Balance -= amount;
            if (Balance < 0)
                OnCreditUsage?.Invoke($"Используется кредит. Снято: ${amount}. Баланс: ${Balance}");
            else
                OnWithdraw?.Invoke($"Снято: ${amount}. Баланс: ${Balance}");
        }
        else
        {
            Console.WriteLine("Недостаточно средств или некорректная сумма.");
        }
    }

    public void CheckBalanceGoal(decimal targetBalance)
    {
        if (targetBalance > 0 && Balance >= targetBalance)
            OnBalanceGoalReached?.Invoke($"Целевой баланс ${targetBalance} достигнут.");
        else
            Console.WriteLine($"Не хватает ${targetBalance - Balance} до целевого баланса.");
    }

    public void UpdatePin(int newPin)
    {
        if (newPin != Pin)
        {
            Pin = newPin;
            OnPinUpdated?.Invoke("ПИН обновлен.");
        }
        else
        {
            Console.WriteLine("Новый ПИН не должен совпадать с текущим.");
        }
    }
}
