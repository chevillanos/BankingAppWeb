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
        private readonly IAccountRepo accountRepo;
        private readonly IHtmlHelper _htmlHelper;

        [BindProperty]
        public Transaction Transaction { get; set; }
        [BindProperty]
        public string AccountTypeName { get; set; }
        [BindProperty]
        public int SelectedCustomerId { get; set; }
        public IEnumerable<SelectListItem> TransactionTypes { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
        public IList<SelectListItem> CustomerList { get; set; } = new List<SelectListItem>();
        public IList<SelectListItem> AccountList { get; set; } = new List<SelectListItem>();

        public AddModel(ITransactionRepo transactionRepo, ICustomerRepo customerRepo, IAccountRepo accountRepo,
            IHtmlHelper htmlHelper)
        {
            _transactionRepo = transactionRepo;
            _customerRepo = customerRepo;
            this.accountRepo = accountRepo;
            _htmlHelper = htmlHelper;
        }

        public IActionResult OnGet()
        {
            TransactionTypes = _htmlHelper.GetEnumSelectList<TranType>();
            Customers = _customerRepo.GetAll();
            GetCustomersList();

            if (Customers != null)
            {
                SelectedCustomerId = Customers.FirstOrDefault().CustomerId;
                PopulateAccountList(SelectedCustomerId);
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            var testOne = SelectedCustomerId;
            if (!string.IsNullOrEmpty(AccountTypeName))
                Transaction.AccountId = (int)Enum.Parse(typeof(AccountType), AccountTypeName);

            var tranAcccount = accountRepo.GetAccountByCustomerIdAndAccountType(SelectedCustomerId,
                AccountTypeName);
            Transaction.AccountId = tranAcccount.AccountId;

            if (!ModelState.IsValid)
            {
                TransactionTypes = _htmlHelper.GetEnumSelectList<TranType>();
                Customers = _customerRepo.GetAll();
                GetCustomersList();

                if (Customers != null)
                {
                    PopulateAccountList(SelectedCustomerId);
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
            var customer = _customerRepo.GetAll().Where(c => c.CustomerId == customerId).FirstOrDefault();
            var accounts = accountRepo.GetAll().Where(a => a.CustomerId == customerId).ToList();
            GetAccountsList(accounts);

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

        //private void GetAccountsList(Customer customer)
        //{
        //    var test = customer.CustomerAccounts;

        //    if (customer.CustomerAccounts != null)
        //    {
        //        foreach (var account in customer.CustomerAccounts)
        //        {
        //            var item = new SelectListItem
        //            {
        //                Text = Enum.GetName(account.Account.AccountType),
        //                Value = account.Account.AccountType.ToString()
        //            };
        //            AccountList.Add(item);
        //        }
        //    }
        //}

        private void GetAccountsList(List<Account> accounts)
        {

            foreach (var account in accounts)
            {
                var item = new SelectListItem
                {
                    Text = Enum.GetName(account.AccountType),
                    Value = account.AccountType.ToString()
                };
                AccountList.Add(item);
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
