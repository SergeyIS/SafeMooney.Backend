using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResourcesLibrary.AccountResources;
using SharedResourcesLibrary.TransactionResources;

namespace DataAccessLibrary
{
    public class DataStorageEmulator
    {
        private static List<User> usersTable;
        private static List<Transaction> transactionsTable;

        static DataStorageEmulator()
        {
            usersTable = new List<User>()
            {
                new User() { Id = 0, Login = "sergey", Password = "123", TokenKey= "abcdefg" },
                new User() { Id = 1, Login = "log", Password = "pas" },
                new User() { Id = 2, Login = "log", Password = "pas" },
                new User() { Id = 3, Login = "log", Password = "pas" },
                new User() { Id = 4, Login = "log", Password = "pas" },
            };

            transactionsTable = new List<Transaction>()
            {
                new Transaction {transactionId = 0, user1Id = 0, user2Id = 1, count = "124.4$",
                    date = DateTime.Now, period = 30, isPermited = false }
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
        public User FindUserById(int id)
        {
            if (id < 0)
                throw new ArgumentException();

            if (id >= usersTable.Count)
                return null;

            return usersTable[id];
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
        public bool CheckForUser(String username)
        {
            if (username == null)
                throw new ArgumentException();

            return usersTable.Contains(new User() { Login = username });
        }
        public void AddUser(String username, String password, String firstName, String lastName)
        {
            int id = usersTable.Count;
            User user = new User() {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Login = username,
                Password = password 
            };

            usersTable.Add(user);
        }
        public void AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException();

            AddUser(user.Login, user.Password, user.FirstName, user.LastName);
        }

        public List<User> GetAllUsers()
        {
            User[] userArray = new User[usersTable.Count];
            usersTable.CopyTo(userArray);
            return new List<User>(userArray);
        }

        public void AddTransaction(Transaction trans)
        {
            if (trans == null)
                throw new ArgumentNullException();

            trans.isPermited = false;
            transactionsTable.Add(trans);
        }

        public List<Transaction> GetTransactionsForUser(int userID)
        {
            /*
             Предполагается, что user1Id инициатор, а user2Id тот, на кого заводят транзакцию
             */
            return transactionsTable.Where(t => t.isPermited == false && t.user1Id == userID).ToList();
        }

        public bool ConfirmTransaction(Transaction trans)
        {
            if (trans == null)
                throw new ArgumentNullException();


            //transaction that belongs to user
            Transaction localTrans = transactionsTable.Where(t => t.transactionId == trans.transactionId && 
            t.user1Id == trans.user1Id && t.user2Id == trans.user2Id).First();

            if (localTrans == null)
                return false;

            localTrans.isPermited = true;

            return true;
        }

        public bool ResetTokenForUser(int userId)
        {
            if (userId < 0 || userId > usersTable.Count)
                return false;

            usersTable[userId].TokenKey = null;

            return true;
        }
    }
}
