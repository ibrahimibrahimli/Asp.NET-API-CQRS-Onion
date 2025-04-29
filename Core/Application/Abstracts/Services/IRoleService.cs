using Application.DTOs.Role;

namespace Application.Abstractions.Services
{
    public interface IRoleService
    {
        IDictionary<string, string> GetAllRoles(int page, int size);
        Task<(string id, string name)> GetRoleById(string id);
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> DeleteRoleAsync(string roleName);
        Task<bool> UpdateRoleAsync(string id, string roleName);
    }
}
