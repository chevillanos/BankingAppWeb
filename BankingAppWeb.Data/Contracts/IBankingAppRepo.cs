using System;
using System.Collections;
using System.Collections.Generic;

namespace BankingAppWeb.Data
{
    public interface IBankingAppRepo<T> where T : class
    {
        T GetById(int id);
        T Add(T entity);
        int Commit();
    }
}
