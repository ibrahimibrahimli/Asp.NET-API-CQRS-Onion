using Application.Abstractions.Services;
using Application.DTOs.Role;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Persistance.Services
{
    public class RoleService : IRoleService
    {
        readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            IdentityResult result =  await _roleManager.CreateAsync(new AppRole {Id = Guid.NewGuid().ToString(), Name = roleName });
            return result.Succeeded;
        }

        public async Task<bool> DeleteRoleAsync(string roleName)
        {
             IdentityResult result = await _roleManager.DeleteAsync(new AppRole { Name = roleName });
            return result.Succeeded;
        }

        public IDictionary<string, string> GetAllRoles(int page, int size)
        {
            return _roleManager.Roles.Skip(page * size).Take(size).ToDictionary(role => role.Id, role => role.Name);  
        }

        public async Task<(string id, string name)> GetRoleById(string id)
        {
            string role = await _roleManager.GetRoleIdAsync(new AppRole { Id = id });
            return (id, role);
        }

        public async Task<bool> UpdateRoleAsync(string id, string roleName)
        {
            IdentityResult result = await _roleManager.UpdateAsync(new AppRole { Id = id, Name = roleName }); 
            return result.Succeeded;
        }

        
    }
}
