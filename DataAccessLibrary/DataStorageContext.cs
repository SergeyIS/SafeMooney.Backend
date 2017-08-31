using System;
using System.Data.Entity;
using SharedResourcesLibrary;

namespace DataAccessLibrary
{
    internal class DataStorageContext : DbContext
    {
        private static String connection = "default";

        public DataStorageContext() : base(connection)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UserImage> UserImages { get; set; }

    }
}
