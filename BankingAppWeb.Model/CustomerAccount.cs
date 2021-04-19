using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppWeb.Model
{
    public class CustomerAccount
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }


    }
}
