using System;
using System.Data.Entity;
using SharedResourcesLibrary;

namespace DataAccessLibrary
{
    internal class DataStorageContext : DbContext
    {

        public DataStorageContext() : base("default")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<AuthService> AuthServices { get; set; }
        public DbSet<AuthProvider> AuthProviders { get; set; }

    }
}
