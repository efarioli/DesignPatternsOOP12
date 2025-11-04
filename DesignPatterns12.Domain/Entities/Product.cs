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
        public string Name { get; protected set; } = string.Empty;
        public decimal Price { get; private set; }
        public bool IsActive { get; private set; } = true;

        public Product() { }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public void Deactivate() => IsActive = false;

        public void UpdateName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
                Name = name;
        }

        public void UpdatePrice(decimal? price)
        {
            if (price.HasValue && price.Value >= 0)
                Price = price.Value;
        }
    }
}
