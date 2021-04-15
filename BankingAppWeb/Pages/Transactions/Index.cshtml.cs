using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingAppWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using model = BankingAppWeb.Model;

namespace BankingAppWeb.Pages.Transactions
{
    public class IndexModel : PageModel
    {
        private readonly ITransactionRepo _transactionRepo;

        public IEnumerable<model.Transaction> Transactions { get; set; }
        public IndexModel(ITransactionRepo transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }
        public void OnGet()
        {
            Transactions = _transactionRepo.GetTransactionsAll();
        }
    }
}
