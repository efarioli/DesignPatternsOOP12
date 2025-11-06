using DesignPatterns12.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns12.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task CreateAsync(User user);
        Task<bool> UpdateAsync(Guid id, User updatedUser);
        Task<bool> DeleteAsync(Guid id);
    }
}
