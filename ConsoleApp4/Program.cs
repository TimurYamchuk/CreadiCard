using System;

public delegate void AccountEventHandler(string message);

public class CreditCard
{
    public string CardNumber { get; set; }
    public string OwnerName { get; set; }
    public DateTime ExpirationDate { get; set; }
    private int Pin { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal Balance { get; set; }

    public event AccountEventHandler OnDeposit;
    public event AccountEventHandler OnWithdraw;
    public event AccountEventHandler OnCreditUsageStart;
    public event AccountEventHandler OnTargetBalanceReached;
    public event AccountEventHandler OnPinChanged;

    public CreditCard(string cardNumber, string ownerName, DateTime expirationDate, int pin, decimal creditLimit, decimal initialBalance)
    {
        CardNumber = cardNumber;
        OwnerName = ownerName;
        ExpirationDate = expirationDate;
        Pin = pin;
        CreditLimit = creditLimit;
        Balance = initialBalance;
    }

    public void ShowCardInfo()
    {
        Console.WriteLine("\n--- Информация о карте ---");
        Console.WriteLine($"Номер карты: {CardNumber}");
        Console.WriteLine($"Владелец: {OwnerName}");
        Console.WriteLine($"Дата истечения: {ExpirationDate.ToShortDateString()}");
        Console.WriteLine($"Текущий баланс: ${Balance}");
        Console.WriteLine($"Кредитный лимит: ${CreditLimit}");
    }

    public void Deposit(decimal amount)
    {
        if (amount > 0)
        {
            Balance += amount;
            OnDeposit?.Invoke($"Пополнено на ${amount}. Новый баланс: ${Balance}");
        }
        else
        {
            Console.WriteLine("Сумма пополнения должна быть положительной.");
        }
    }

    public void Withdraw(decimal amount)
    {
        if (amount > 0 && amount <= Balance + CreditLimit)
        {
            Balance -= amount;
            if (Balance < 0)
            {
                OnCreditUsageStart?.Invoke($"Вы начали использовать кредит. Снято: ${amount}. Баланс: ${Balance}");
            }
            else
            {
                OnWithdraw?.Invoke($"Снято: ${amount}. Новый баланс: ${Balance}");
            }
        }
        else
        {
            Console.WriteLine("Недостаточно средств.");
        }
    }

    public void CheckTargetBalance(decimal targetBalance)
    {
        if (Balance >= targetBalance)
        {
            OnTargetBalanceReached?.Invoke($"Целевой баланс ${targetBalance} достигнут.");
        }
        else
        {
            Console.WriteLine($"Необходимо ещё ${targetBalance - Balance} для достижения целевого баланса.");
        }
    }

    public void ChangePin(int newPin)
    {
        if (newPin != Pin)
        {
            Pin = newPin;
            OnPinChanged?.Invoke("ПИН успешно изменён.");
        }
        else
        {
            Console.WriteLine("Новый ПИН не может совпадать с текущим.");
        }
    }
}
