using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppWeb.Model
{
    public class Account
    {
        public int AccountId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public AccountType AccountType { get; set; }

        public IList<CustomerAccount> CustomerAccounts { get; set; }
    }
}
