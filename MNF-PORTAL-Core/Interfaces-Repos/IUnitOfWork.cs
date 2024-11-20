using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNF_PORTAL_Core.Interfaces_Repos
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
      //  IRoleRepository RoleRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
