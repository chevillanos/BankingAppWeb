using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BankingAppWeb.Data;
using BankingAppWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankingAppWeb.Pages.Transactions
{
    public class AddModel : PageModel
    {
        private readonly ITransactionRepo _transactionRepo;
        private readonly ICustomerRepo _customerRepo;
        private readonly IHtmlHelper _htmlHelper;

        [BindProperty]
        public Transaction Transaction { get; set; }
        [BindProperty]
        public string AccountTypeName { get; set; }
        public IEnumerable<SelectListItem> TransactionTypes { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IList<SelectListItem> CustomerList { get; set; } = new List<SelectListItem>();
        public IList<SelectListItem> AccountList { get; set; } = new List<SelectListItem>();

        public AddModel(ITransactionRepo transactionRepo, ICustomerRepo customerRepo,
            IHtmlHelper htmlHelper)
        {
            _transactionRepo = transactionRepo;
            _customerRepo = customerRepo;
            _htmlHelper = htmlHelper;
        }

        public IActionResult OnGet()
        {
            TransactionTypes = _htmlHelper.GetEnumSelectList<TranType>();
            Customers = _customerRepo.GetAllCustomers();
            GetCustomersList();

            if (Customers != null)
            {
                var selectedCustomer = Customers.FirstOrDefault();
                PopulateAccountList(selectedCustomer.CustomerId);
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrEmpty(AccountTypeName))
                Transaction.AccountId = (int)Enum.Parse(typeof(AccountType), AccountTypeName);

            if (!ModelState.IsValid)
            {
                TransactionTypes = _htmlHelper.GetEnumSelectList<TranType>();
                Customers = _customerRepo.GetAllCustomers();
                GetCustomersList();

                if (Customers != null)
                {
                    var selectedCustomer = Customers.FirstOrDefault();
                    PopulateAccountList(selectedCustomer.CustomerId);
                }

                return Page();
            }

            _transactionRepo.Add(Transaction);
            _transactionRepo.Commit();
            TempData["Message"] = "Transaction saved!";
            return RedirectToPage("/Index");
        }

        public IEnumerable<SelectListItem> PopulateAccountList(int customerId)
        {
            var customer = _customerRepo.GetAllCustomers().Where(c => c.CustomerId == customerId).FirstOrDefault();
            GetAccountsList(customer);

            return AccountList;
        }

        private void GetCustomersList()
        {
            foreach (var customer in Customers)
            {
                var item = new SelectListItem
                {
                    Text = customer.CustomerName,
                    Value = customer.CustomerId.ToString()
                };
                CustomerList.Add(item);
            }
        }

        private void GetAccountsList(Customer customer)
        {
            if (customer.Accounts != null)
            {
                foreach (var account in customer.Accounts)
                {
                    var item = new SelectListItem
                    {
                        Text = Enum.GetName(account.AccountType),
                        Value = account.AccountType.ToString()
                    };
                    AccountList.Add(item);
                }
            }
        }

        public IActionResult OnPostAccounts()
        {
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyToAsync(stream);
            stream.Position = 0;
            using StreamReader reader = new StreamReader(stream);
            string requestBody = reader.ReadToEnd();
            if (requestBody.Length > 0)
            {
                return new JsonResult(PopulateAccountList(int.Parse(requestBody)));
            }

            return null;
        }
    }
}
