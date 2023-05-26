using Sut.IRepositories;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace Sut.Repositories
{
    public class Context : IContext, IUnitOfWork, IDisposable
    {
        private readonly DbContext _dataContext;

        public Context(DbContext context)
        {
            _dataContext = context;
        }
        public DbContext GetContext()
        {
            return _dataContext;
        }

        public int SaveChanges(bool validate = true)
        {
            if (!validate)
            {
                _dataContext.Configuration.ValidateOnSaveEnabled = false;
            }
            try
            {
                return _dataContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        public void RollBack()
        {
            foreach (DbEntityEntry entry in _dataContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    // Under the covers, changing the state of an entity from  
                    // Modified to Unchanged first sets the values of all  
                    // properties to the original values that were read from  
                    // the database when it was queried, and then marks the  
                    // entity as Unchanged. This will also reject changes to  
                    // FK relationships since the original value of the FK  
                    // will be restored. 
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    // If the EntityState is the Deleted, reload the date from the database.   
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                    default: break;
                }
            }
        }

        #region IDisposable
        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                if (_dataContext != null)
                {
                    _dataContext.Dispose();
                }
            }

            _disposed = true;
        }

        
        #endregion
    }
}
