using System;
using System.Collections.Generic;

namespace Sut.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        T GetOne(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        List<T> GetAll();
        List<T> GetAllBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        int CountBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void Save(T entity);
        void Delete(T entity);
    }
}
