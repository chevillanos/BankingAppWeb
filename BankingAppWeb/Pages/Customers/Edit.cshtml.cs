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
    public class EditModel : PageModel
    {
        private readonly ICustomerRepo customerRepo;

        [BindProperty]
        public Customer Customer { get; set; }

        public EditModel(ICustomerRepo customerRepo)
        {
            this.customerRepo = customerRepo;
        }

        public IActionResult OnGet(int? customerId)
        {
            if (customerId.HasValue)
                Customer = customerRepo.GetById(customerId.Value);
            else
                Customer = new Customer();

            if (Customer == null)
                return RedirectToPage("/Error");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            // Reuse
            if (Customer.CustomerId > 0)
                customerRepo.Update(Customer);
            else
                customerRepo.Add(Customer);

            customerRepo.Commit();
            TempData["Message"] = "Customer saved!";
            return RedirectToPage("/Index");
            //return RedirectToPage("/Index", new { customerId = Customer.CustomerId });
        }
    }
}
