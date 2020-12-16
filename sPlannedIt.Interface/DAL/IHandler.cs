using System;
using System.Collections.Generic;
using System.Text;

namespace sPlannedIt.Interface.DAL
{
    public interface IHandler<T> where T: class
    {
        List<T> GetAll();
        T Create(T entity);
        T Update (T entity);
        bool Delete(string id);
        T GetById(string id);

    }
}
