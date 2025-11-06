using DesignPatterns12.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns12.Domain.Entities
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        IRepository<User> Users { get; }
        Task<int> CompleteAsync();
    }
}
