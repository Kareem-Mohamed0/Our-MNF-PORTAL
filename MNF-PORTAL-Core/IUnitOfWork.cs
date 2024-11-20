using MNF_PORTAL_Core.Interfaces_Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNF_PORTAL_Core
{
    public interface IUnitOfWork
    {
        public IRoleRepository RoleRepository { get; }
        Task<bool> CompleteAsync();
    }
}
