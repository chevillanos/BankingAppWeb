using BankingAppWeb.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppWeb.Data
{
    public class TransactionRepo : BankingAppRepo<Transaction>, ITransactionRepo
    {
        private readonly BankingAppWebDbContext _db;
        public TransactionRepo(BankingAppWebDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Transaction> GetTransactionsAll()
        {
            return _db.Transactions.Include(t => t.Account)
                .ThenInclude(a => a.Customer).ToList();
        }
    }
}
