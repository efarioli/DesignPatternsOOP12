using DesignPatterns12.Application.Interfaces;
using DesignPatterns12.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns12.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _unitOfWork.Products.GetAllAsync();
        }
        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.Products.GetByIdAsync(id);
        }
        public async Task CreateAsync(Product product)
        {
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync();
        }
        public async Task<bool> UpdateAsync(Guid id, Product updatedProduct)
        {
            var existing = await _unitOfWork.Products.GetByIdAsync(id);
            if (existing is null) return false;

            existing.UpdateName(updatedProduct.Name);
            existing.UpdatePrice(updatedProduct.Price);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _unitOfWork.Products.GetByIdAsync(id);
            if (existing is null) return false;

            _unitOfWork.Products.Delete(existing);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
