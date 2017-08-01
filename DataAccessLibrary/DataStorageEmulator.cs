using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResourcesLibrary.AccountResources;
using SharedResourcesLibrary.TransactionResources;

namespace DataAccessLibrary
{
    public class DataStorageEmulator : IDataAccess
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

            transactionsArchive = new List<Transaction>();

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
            var query = usersTable.Where(u => u.Id == id);

            if (query.Count() == 0)
                return null;

            if (query.Count() > 1)
                throw new Exception("There's more than one user in the database with such parameters");

            return query.First();
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
        public void AddUserSafely(String username, String password, String firstName, String lastName)
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
        public void AddUserSafely(User user)
        {
            if (user == null)
                throw new ArgumentNullException();

            AddUserSafely(user.Login, user.Password, user.FirstName, user.LastName);
        }
        public bool AddUser(User user)
        {
            var r = FindUserById(user.Id);
            if (FindUserById(user.Id) != null)
                return false;

            usersTable.Add(user);

            return true;
        }
        public bool RemoveUser(int userId, ref String token)
        {
            User user =  this.FindUserById(userId);

            if (user == null)
                return false;

            token = user.TokenKey;
            usersTable.Remove(user);

            return true;
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
        public List<Transaction> GetTransactionsForUser(int userId)
        {
            /*
             Предполагается, что user1Id инициатор, а user2Id тот, на кого заводят транзакцию
             */
            var query = transactionsTable.Where(t => t.isPermited == false && t.user2Id == userId);
            if (query.Count() == 0)
                return null;

            return query.ToList();
        }
        public bool ConfirmTransaction(Transaction trans)
        {
            if (trans == null)
                throw new ArgumentNullException();


            //transaction that belongs to user
            var query = transactionsTable.Where(t => t.transactionId == trans.transactionId && 
            t.user1Id == trans.user1Id && t.user2Id == trans.user2Id);

            if (query.Count() == 0)
                return false;

            Transaction localTrans = query.First();
            
            localTrans.isPermited = true;

            return true;
        }
        public bool CloseTransactionForUser(int transId, int userId)
        {
            //todo: implement IEquatable<Transaction> for more flexibility
            var query = transactionsTable.Where(t => t.transactionId == transId && t.user1Id == userId);

            int count = query.Count();
            if (count == 0 || count > 1)
                return false;

            Transaction localTrans = query.First();
            localTrans.isClosed = true;
            
            return true;
        }
        public bool ResetTokenForUser(int userId)
        {
            if (userId < 0 || userId > usersTable.Count)
                return false;

            usersTable[userId].TokenKey = null;

            return true;
        }
        public List<Transaction> FetchTransactions(int userId)
        {
            /*
             Предполагается, что user1Id инициатор, а user2Id тот, на кого заводят транзакцию
             */
            var query = transactionsTable.Where(t => t.isPermited == false && t.user1Id == userId);
            if (query.Count() == 0)
                return null;

            return query.ToList();
        }
    }
}
