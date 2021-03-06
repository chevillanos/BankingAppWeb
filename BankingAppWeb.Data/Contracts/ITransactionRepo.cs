using BankingAppWeb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppWeb.Data
{
    public interface ITransactionRepo : IBankingAppRepo<Transaction>
    {
        IEnumerable<Transaction> GetTransactionsAll();
        IEnumerable<Transaction> GetTransactionMyAmt(decimal tranAmt);
    }
}
