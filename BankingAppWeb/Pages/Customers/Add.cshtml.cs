using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingAppWeb.Data;
using BankingAppWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankingAppWeb.Pages.Customers
{
    public class AddModel : PageModel
    {
        private readonly ICustomerRepo _customerRepo;

        [BindProperty]
        public Customer Customer { get; set; }

        public AddModel(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public IActionResult OnGet()
        {
            Customer = new Customer();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _customerRepo.Add(Customer);
            _customerRepo.Commit();
            TempData["Message"] = "Customer saved!";
            return RedirectToPage("/Index");
        }
    }
}
