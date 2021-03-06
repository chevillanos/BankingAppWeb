using BankingAppWeb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppWeb.Data
{
    public interface IAccountRepo : IBankingAppRepo<Account>
    {
        Account GetAccountById(int id);
        Account GetAccountByCustomerIdAndAccountType(int customerId, string accountType);
    }
}
