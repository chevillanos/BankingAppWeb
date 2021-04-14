using BankingAppWeb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppWeb.Data
{
    public class CustomerRepo : BankingAppRepo<Customer>, ICustomerRepo
    {
        public CustomerRepo(BankingAppWebDbContext db) : base(db)
        {
        }
    }
}
