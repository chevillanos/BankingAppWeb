using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingAppWeb.Data;
using BankingAppWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankingAppWeb.Pages.Accounts
{
    public class DetailModel : PageModel
    {
        private readonly IAccountRepo _accountRepo;

        [TempData]
        public string Message { get; set; }
        public Account Account { get; set; }

        public DetailModel(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public IActionResult OnGet(int accountId)
        {
            Account = _accountRepo.GetById(accountId);
            if (Account == null)
                return RedirectToPage("/NotFound");
            return Page();
        }
    }
}
