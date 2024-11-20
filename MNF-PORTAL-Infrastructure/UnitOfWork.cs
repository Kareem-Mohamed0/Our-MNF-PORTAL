using Microsoft.AspNetCore.Identity;
using MNF_PORTAL_Core;
using MNF_PORTAL_Core.Entities;
using MNF_PORTAL_Core.Interfaces_Repos;
using MNF_PORTAL_Infrastructure.Data;
using MNF_PORTAL_Infrastructure.Repositories;

namespace MNF_PORTAL_Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        public IRoleRepository RoleRepository { get; set; }
        public UnitOfWork(AppDbContext context,IRoleRepository roleRepository)
        {
            this.RoleRepository = roleRepository; 
            this.context = context;
        }

        

        public async Task<bool> CompleteAsync()
        {
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
    }
}
