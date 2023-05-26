namespace Sut.IRepositories
{
    public interface IUnitOfWork
    {
        int SaveChanges(bool validate = true);

        void RollBack();
    }
}
