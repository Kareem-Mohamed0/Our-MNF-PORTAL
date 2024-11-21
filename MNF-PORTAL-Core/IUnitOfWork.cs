using MNF_PORTAL_Core.Interfaces_Repos;
namespace MNF_PORTAL_Core
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        public IRoleRepository RoleRepository { get; }
        Task<bool> CompleteAsync();
    }
}
