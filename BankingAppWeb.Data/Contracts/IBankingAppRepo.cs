using System;
using System.Collections;
using System.Collections.Generic;

namespace BankingAppWeb.Data
{
    public interface IBankingAppRepo<T> where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        T Add(T entity);
        T Update(T entity);
        int Commit();
    }
}
