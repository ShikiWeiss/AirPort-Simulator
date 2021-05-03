using System;
using System.Collections.Generic;
using System.Text;

namespace Airport.Dal.Api
{
    public interface IRepository<T> where T : class
    {
        bool Add(T entity);

        T GetById(int id);

        IEnumerable<T> GetAll();
    }
}
