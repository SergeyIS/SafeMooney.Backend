using System.Data.Entity;
using SafeMooney.Shared.Models;

namespace SafeMooney.DAL
{
    internal class DataContext : DbContext
    {

        public DataContext() : base("default")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<AuthService> AuthServices { get; set; }
        public DbSet<AuthProvider> AuthProviders { get; set; }

    }
}
