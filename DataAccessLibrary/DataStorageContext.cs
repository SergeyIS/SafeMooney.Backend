using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
