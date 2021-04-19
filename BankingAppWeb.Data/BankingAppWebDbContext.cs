using BankingAppWeb.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppWeb.Data
{
    public class BankingAppWebDbContext : DbContext
    {
        public BankingAppWebDbContext(DbContextOptions<BankingAppWebDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerAccount>().HasKey(ca => new { ca.CustomerId, ca.AccountId });

            modelBuilder.Entity<CustomerAccount>()
                .HasOne<Customer>(ca => ca.Customer)
                .WithMany(c => c.CustomerAccounts)
                .HasForeignKey(ca => ca.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerAccount>()
                .HasOne<Account>(ca => ca.Account)
                .WithMany(a => a.CustomerAccounts)
                .HasForeignKey(ca => ca.AccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

    }
}
