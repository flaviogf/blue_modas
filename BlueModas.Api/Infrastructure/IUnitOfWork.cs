namespace BlueModas.Api.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
