using Autofac;
using BusinessLogicLeve;
using Microsoft.EntityFrameworkCore;
using Projects.Entities;

var programLoop = true;
List<Account> accounts;
var _container = ContainerConfig.Configure().Build();

while (programLoop)
{
    Operation();
}

void Operation()
{
    using (var scope = _container.BeginLifetimeScope())
    using (var bankService = scope.Resolve<BankService>())
    {
        accounts = new List<Account>(bankService.GetAllWallets());
        Console.Clear();
        foreach (var item in accounts)
        {
            Console.WriteLine($"Account #{item.Id} \n Fortune - {item.Fortune}\n_____\n");
        }
        var accountId = GetInputToLength($"Choose account number", accounts.Count + 1, 0);
        Console.Clear();
        var choiceNumber = GetInputToLength($"Input 1 for transaction between accounts and 2 for the income items list", 3);
        if (choiceNumber == 1)
            TransactionProcedure(accountId, bankService);
        else if (choiceNumber == 2)
            IncomeItemsProcedure(accountId, bankService);
        bankService.Save();
    }

}

void IncomeItemsProcedure(int currAccount, BankService service)
{
    var incomes = service.GetIncomesByWallet(currAccount).ToList();
    foreach (var item in incomes)
    {
        Console.WriteLine($"Income #{item.Id} \n Name - {item.Name}\n_____\n");
    }
    var incomeId = GetInputToLength($"Choose income number with which you want to hold a transaction", incomes[incomes.Count - 1].Id + 1, incomes[0].Id - 1);

    TryIncomeTransaction(currAccount, service, incomeId);
}

void TransactionProcedure(int currAccount, BankService service)
{
    foreach (var item in accounts)
    {
        Console.WriteLine($"Account #{item.Id} \n Fortune - {item.Fortune}\n_____\n");
    }
    var accountName = GetInputToLength($"Choose account number with which you want to hold a transaction", accounts.Count + 1, 0);

    TryTransaction(currAccount, service, accountName);
}

int GetInputToLength(string request, int length, int low = 0)
{
    int output = -1;
    while (true)
    {
        Console.WriteLine(request);
        var input = Console.ReadLine();
        if (!int.TryParse(input, out output))
            Console.WriteLine("Invalid operation number");
        else
        {
            if (output > low && output < length)
                break;
            else
                Console.WriteLine($"number must be greater than 0 and less than {length}");
        }
    }
    return output;
}

void TryTransaction(int currAccount, BankService service, int accountName)
{
    var sum = GetInputToLength("\nChoose a sum | Positive will add to chosen account and vise versa", int.MaxValue, int.MinValue);
    try { service.Transaction(currAccount, accountName, sum); }
    catch
    {
        Console.WriteLine("Sum is out of bounds");
    }
}

void TryIncomeTransaction(int currAccount, BankService service, int incomeId)
{
    Console.Clear();
    Console.WriteLine("Transaction history:");
    var transactions = service.GetTransactionsByIncomeItem(incomeId).Include(n => n.TransactionType);
    foreach (var item in transactions)
    {
        var type = item.TransactionTypeId == 1 ? "Income" : "Expenditure";
        Console.WriteLine($"Transaction #{item.Id} \n Sum - {item.Sum}\n Type - {type}\n_____");
    }
    var sum = GetInputToLength("\nChoose a sum | Positive will add to chosen account and vise versa", int.MaxValue, int.MinValue);
    try  { service.IncomeChange(currAccount, incomeId, sum); }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
