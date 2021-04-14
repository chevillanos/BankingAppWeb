using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingAppWeb.Data;
using BankingAppWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankingAppWeb.Pages.Customers
{
    public class DetailModel : PageModel
    {
        private readonly ICustomerRepo _customerRepo;

        [TempData]
        public string Message { get; set; }
        public Customer Customer { get; set; }

        public DetailModel(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public IActionResult OnGet(int customerId)
        {
            Customer = _customerRepo.GetById(customerId);
            if (Customer == null)
                return RedirectToPage("./NotFound");
            return Page();
        }
    }
}
