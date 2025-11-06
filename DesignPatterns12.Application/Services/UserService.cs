using DesignPatterns12.Application.Interfaces;
using DesignPatterns12.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns12.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetAllAsync() =>
            await _unitOfWork.Users.GetAllAsync();

        public async Task<User?> GetByIdAsync(Guid id) =>
            await _unitOfWork.Users.GetByIdAsync(id);

        public async Task CreateAsync(User user)
        {
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> UpdateAsync(Guid id, User updatedUser)
        {
            var existing = await _unitOfWork.Users.GetByIdAsync(id);
            if (existing == null) return false;

            existing.UpdateEmail(updatedUser.Email);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _unitOfWork.Users.GetByIdAsync(id);
            if (existing == null) return false;

            _unitOfWork.Users.Remove(existing);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
