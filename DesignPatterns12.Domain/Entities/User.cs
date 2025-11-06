using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns12.Domain.Entities
{
    public class User
    {
        // Unique identifier for the user
        public Guid Id { get; private set; }

        // Initialized with null! to satisfy EF Core non-nullable property checks
        public string Username { get; private set; } = null!;

        // Same fix as above for EF Core warnings
        public string Email { get; private set; } = null!;

        // Indicates whether the user is active
        public bool IsActive { get; private set; }

        // Parameterless constructor required by EF Core
        private User() { }

        // Main constructor used for creating new users in code
        public User(string username, string email)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is required.", nameof(username));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.", nameof(email));

            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            IsActive = true;
        }

        // Marks the user as inactive
        public void Deactivate() => IsActive = false;

        // Updates the email after validation
        public void UpdateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.", nameof(email));

            Email = email;
        }
    }
}
