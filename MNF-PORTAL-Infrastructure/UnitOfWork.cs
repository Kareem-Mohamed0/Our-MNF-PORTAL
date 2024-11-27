using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MNF_PORTAL_Core;
using MNF_PORTAL_Core.Entities;
using MNF_PORTAL_Core.Interfaces_Repos;
using MNF_PORTAL_Infrastructure.Data;
using MNF_PORTAL_Infrastructure.Implementation_Repos;
using MNF_PORTAL_Infrastructure.Repositories;

namespace MNF_PORTAL_Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UnitOfWork(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_userManager,_context);
        public IRoleRepository RoleRepository => _roleRepository ??= new RoleRepository(_roleManager,_context);
        public IGenericRepository<T> Repository<T>() where T : class => new GenericRepository<T>(_context);

        public async Task<bool> CompleteAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
