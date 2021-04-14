using System;
using System.Collections;
using System.Collections.Generic;

namespace BankingAppWeb.Data
{
    public interface IBankingAppRepo<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}
