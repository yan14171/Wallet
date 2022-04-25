using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Projects.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Context
{
    public class WalletDbContextFactory : IDesignTimeDbContextFactory<WalletDbContext>
    {
        public WalletDbContext CreateDbContext(string[]? args = null)
        {
            var configuration = new ConfigurationBuilder().Build();

            var optionsBuilder = new DbContextOptionsBuilder<WalletDbContext>();
            optionsBuilder
                // Uncomment the following line if you want to print generated
                // SQL statements on the console.
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .UseSqlServer("Server=DESKTOP-53S0VPF;Database=Wallet;Trusted_Connection=True");

            return new WalletDbContext(optionsBuilder.Options);
        }
    }
    public class WalletDbContext : DbContext
    {
        public WalletDbContext(DbContextOptions<WalletDbContext> options)
        : base(options)
        { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<IncomeItem> IncomeItems { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(n => n.Incomes)
                .WithOne(b => b.Account)
                .HasForeignKey(b => b.AccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            modelBuilder.Entity<Account>()
                .HasKey(n => n.Id);

            modelBuilder.Entity<IncomeItem>()
                .HasMany(b => b.Transactions)
                .WithOne(t => t.IncomeItem)
                .HasForeignKey(t => t.IncomeItemId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            modelBuilder.Entity<IncomeItem>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Transaction>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<TransactionType>()
                .HasMany(m => m.Transactions)
                .WithOne(t => t.TransactionType)
                .HasForeignKey(t => t.TransactionTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);

            modelBuilder.Entity<TransactionType>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<TransactionType>().HasData(new TransactionType() { Id = 1, Name = "Income" });
            modelBuilder.Entity<TransactionType>().HasData(new TransactionType() { Id = 2, Name = "Expenditure" });
        }
    }
}
