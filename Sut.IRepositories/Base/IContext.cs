using System.Data.Entity;

namespace Sut.IRepositories
{
    public interface IContext
    {
        DbContext GetContext();

        int SaveChanges(bool validate = true);
    }
}
