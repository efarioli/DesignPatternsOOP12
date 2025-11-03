using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns12.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Email { get; private set; } = string.Empty;

        private Customer() { }

        public Customer(string email)
        {
            Email = email;
        }
    }
}
