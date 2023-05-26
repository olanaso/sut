using Sut.Context;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sut.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IContext _context;

        protected IContext Context
        {
            get { return _context; }
        }

        public BaseRepository(IContext context)
        {
            _context = context;
        }

        public T GetOne(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _context.GetContext().Set<T>().SingleOrDefault(predicate);
        }

        public List<T> GetAll()
        {
            return _context.GetContext().Set<T>().ToList<T>();
        }

        public List<T> GetAllBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _context.GetContext().Set<T>().Where(predicate).ToList<T>();
        }

        public int CountBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _context.GetContext().Set<T>().Count(predicate);
        }


        public void Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException("");
            try
            {
                _context.GetContext().Entry(entity).State = EntityState.Added;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException();
            }

        }

        public void Save(T entity)
        {
            if (entity == null) throw new ArgumentNullException("");
            try
            {
                _context.GetContext().Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException();
            }
        }

        public void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("");
            try
            {
                _context.GetContext().Entry(entity).State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
