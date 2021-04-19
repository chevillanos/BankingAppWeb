using BankingAppWeb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BankingAppWeb.Data
{
    public class AccountRepo : BankingAppRepo<Account>, IAccountRepo
    {
        private readonly BankingAppWebDbContext _db;
        public AccountRepo(BankingAppWebDbContext db) : base(db)
        {
            _db = db;
        }

        public Account GetAccountByCustomerIdAndAccountType(int customerId, string accountType)
        {
            var accountTypes = Enum.GetNames(typeof(AccountType)).ToList();
            return _db.Accounts.Where(a => a.CustomerId == customerId && accountTypes.Contains(accountType)).FirstOrDefault();
        }

        public Account GetAccountById(int id)
        {
            return _db.Accounts.Include(a => a.CustomerAccounts).FirstOrDefault();
        }
    }
}
