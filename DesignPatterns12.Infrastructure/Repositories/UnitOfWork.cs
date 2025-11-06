using DesignPatterns12.Application.Interfaces;
using DesignPatterns12.Domain.Entities;
using DesignPatterns12.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns12.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IRepository<Product> Products { get; }

        public IRepository<User> Users { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Products = new Repository<Product>(_context);
            Users = new Repository<User>(_context);
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }

}
