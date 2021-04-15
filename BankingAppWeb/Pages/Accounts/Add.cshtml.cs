using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingAppWeb.Data;
using BankingAppWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankingAppWeb.Pages.Accounts
{
    public class AddModel : PageModel
    {
        private readonly IAccountRepo _accountRepo;
        private readonly ICustomerRepo _customerRepo;
        private readonly IHtmlHelper _htmlHelper;

        [BindProperty]
        public Account Account { get; set; }
        public IEnumerable<SelectListItem> AccountTypes { get; set; }
        public IList<SelectListItem> Customers { get; set; } = new List<SelectListItem>();

        public AddModel(IAccountRepo accountRepo, ICustomerRepo customerRepo, IHtmlHelper htmlHelper)
        {
            _accountRepo = accountRepo;
            _customerRepo = customerRepo;
            _htmlHelper = htmlHelper;
        }

        public IActionResult OnGet()
        {
            AccountTypes = _htmlHelper.GetEnumSelectList<AccountType>();
            GetCustomersList();

            return Page();            
        }

        public IActionResult OnPost()
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (!ModelState.IsValid)
            {
                AccountTypes = _htmlHelper.GetEnumSelectList<AccountType>();
                GetCustomersList();
                return Page();
            }                

            _accountRepo.Add(Account);
            _accountRepo.Commit();
            TempData["Message"] = "Account saved!";
            return RedirectToPage("/Index");
        }

        private void GetCustomersList()
        {
            var customers = _customerRepo.GetAllCustomers();
            foreach (var customer in customers)
            {
                var item = new SelectListItem
                {
                    Text = customer.CustomerName,
                    Value = customer.CustomerId.ToString()
                };
                Customers.Add(item);
            }
        }
    }
}
