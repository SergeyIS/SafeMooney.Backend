using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResourcesLibrary.AccountResources;

namespace DataAccessLibrary
{
    public class DataStorageEmulator
    {
        private List<User> usersTable;

        public DataStorageEmulator()
        {
            usersTable = new List<User>()
            {
                new User() { Id = 0, Login = "sergey", Password = "pas", TokenKey = "QWxhZGRppvcGVuIHNlc2FtZQ==" },
                new User() { Id = 1, Login = "log", Password = "pas" },
                new User() { Id = 2, Login = "log", Password = "pas" },
                new User() { Id = 3, Login = "log", Password = "pas" },
                new User() { Id = 4, Login = "log", Password = "pas" },
            };
        }

        public User FindUser(String login, String password)
        {
            if (login == null || password == null)
                throw new Exception("It's not allowed to use empty arguments");

            var result = usersTable.Where(u => u.Login.Equals(login) && u.Password.Equals(password)).ToList();

            if (result.Count == 0)
                return null;

            if (result.Count > 1)
                throw new Exception("There's more than one user in the database with such parameters");

            return result.First();

        }
        public User FindUserByLogin(String login)
        {
            if (login == null)
                throw new Exception("It's not allowed to use empty argument");

            var result = usersTable.Where(u => u.Login.Equals(login)).ToList();

            if (result.Count == 0)
                return null;

            if (result.Count > 1)
                throw new Exception("There's more than one user in the database with such parameter");

            return result.First();
        }
        public User FindUserByToken(String TokenKey)
        {
            throw new NotImplementedException();
        }
    }
}
