using Microsoft.EntityFrameworkCore;
using Projects.DataAccess.Interfaces;
using Projects.DataAccess.Repositories;
using Projects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLeve
{
    public class BankService : IDisposable
    {
        private readonly IUnitOfWork _bankUnit;
        public BankService(IUnitOfWork bankUnit)
        {
            this._bankUnit = bankUnit;
        }

        public void Dispose()
        {
            _bankUnit.Dispose();
        }

        public void Save()
        {
            (_bankUnit as DBUnitOfWork).Save();
        }
        public IQueryable<Account> GetAllWallets()
        {
            return _bankUnit.Accounts.GetAll()
                .AsQueryable();
        }
        
        public IQueryable<IncomeItem> GetIncomesByWallet(int accountId)
        {
            return _bankUnit.IncomeItems.GetAll()
                .AsQueryable()
                .Where(n => n.AccountId == accountId);
        }
        
        public IQueryable<Transaction> GetTransactionsByIncomeItem(int incomeItemId)
        {
            return _bankUnit.Transactions.GetAll()
                .AsQueryable()
                .Where(n => n.IncomeItemId == incomeItemId);
        }

        public void Transaction(int accountId1, int accountId2, int sum)
        {
            var chosenAcc = GetAllWallets().First(n => n.Id == accountId1);
            var transactionAcc = GetAllWallets().First(n => n.Id == accountId2);
            var maxSum = (int)transactionAcc.Fortune;
            var minSum = (int)chosenAcc.Fortune * -1;

            if (sum > maxSum || sum < minSum)
                throw new InvalidOperationException("Invalid sum given");

            chosenAcc.Fortune += sum;
            transactionAcc.Fortune -= sum;
        }

        public void IncomeChange(int accountId, int incomeId, int sum)
        {
            var chosenAcc = GetAllWallets().First(n => n.Id == accountId);
            var chosenIncome = GetIncomesByWallet(accountId)
                .Include(n => n.Transactions)
                .FirstOrDefault(n => n.Id == incomeId);

            if (chosenIncome is null)
                throw new NullReferenceException("There is no income with given id in the selected account");

            if (chosenIncome.Transactions is null)
                chosenIncome.Transactions = new List<Transaction>();

            chosenIncome.Transactions.Add(
                new Transaction
                {
                    IncomeItem = chosenIncome,
                    Sum = sum,
                    TransactionTypeId = sum > 0 ? 1 : 2
                });
            chosenAcc.Fortune += sum;
        }
    }
}
