using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppWeb.Model
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
        public int AccountId { get; set; }
        [Required]
        public TranType TranType { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal TranAmt { get; set; }
    }
}
