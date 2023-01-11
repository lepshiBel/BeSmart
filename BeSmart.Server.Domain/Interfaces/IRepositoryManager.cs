namespace BeSmart.Server.Domain.Interfaces
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }
        void Save();
    }
}
