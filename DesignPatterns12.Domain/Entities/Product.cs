using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns12.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public bool IsActive { get; private set; } = true;

        private Product() { }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public void Deactivate() => IsActive = false;
    }
}
