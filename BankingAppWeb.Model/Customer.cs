using System;
using System.ComponentModel.DataAnnotations;

namespace BankingAppWeb.Model
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerAddress { get; set; }
    }
}
