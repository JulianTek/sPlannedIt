using System;
using System.Collections.Generic;
using System.Text;

namespace sPlannedIt.Interface.DAL
{
    public interface IHandler<T> where T: class
    {
        List<T> GetAll();
        void Create(T entity);
        void Update (T entity);
        void Delete(string id);
        T GetById(string id);

    }
}
