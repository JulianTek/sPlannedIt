using System;
using System.Collections.Generic;
using System.Text;

namespace sPlannedIt.Interface.DAL
{
    public interface IHandler<T> where T: class
    {
        List<T> GetAll();
        bool Create(T entity);
        bool Update (T entity);
        bool Delete(string id);
        T GetById(string id);

    }
}
