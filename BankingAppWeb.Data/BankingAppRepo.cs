using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppWeb.Data
{
    public class BankingAppRepo<T> : IBankingAppRepo<T> where T : class
    {
        private readonly BankingAppWebDbContext _db;

        public BankingAppRepo(BankingAppWebDbContext db)
        {
            _db = db;
        }

        public T Add(T entity)
        {
            _db.Add<T>(entity);
            return entity;
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }

        public List<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            var entity = _db.Find<T>(id);
            return entity;
        }

        public T Update(T entity)
        {            
            _db.Update<T>(entity);
            return entity;
        }
    }
}
