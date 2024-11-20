using MNF_PORTAL_Core.Interfaces_Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNF_PORTAL_Infrastructure.Implementation_Repos
{
    public class UnitOfWork : IUnitOfWork
    {
            public IUserRepository UserRepository { get; }
            // public IRoleRepository RoleRepository { get; }

            public UnitOfWork(IUserRepository userRepository)
            {
                UserRepository = userRepository;
                
            }

            public async Task<int> SaveChangesAsync()
            {
                // Implement if working with a DbContext; return dummy value for Identity.
                return 1;
            }

            public void Dispose()
            {
                // Implement if needed.
            }
       
    }
}
