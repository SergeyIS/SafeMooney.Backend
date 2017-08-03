using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResourcesLibrary;

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
                new User() { Id = 0, Username = "sergey", Password = "123", TokenKey= "abcdefg" },
                new User() { Id = 1, Username = "log", Password = "pas" },
                new User() { Id = 2, Username = "log", Password = "pas" },
                new User() { Id = 3, Username = "log", Password = "pas" },
                new User() { Id = 4, Username = "log", Password = "pas" },
            };

            transactionsTable = new List<Transaction>()
            {
                new Transaction {Id = 0, User1Id = 0, User2Id = 1, Count = "124.4$",
                    Date = DateTime.Now, Period = 30, IsPermited = false }
            };

            //transactionsArchive = new List<Transaction>();

        }

        bool SetTokenForUser(int userId, String token) { throw new NotImplementedException(); }

        public User FindUser(String login, String password)
        {
            if (login == null || password == null)
                throw new Exception("It's not allowed to use empty arguments");

            var result = usersTable.Where(u => u.Username.Equals(login) && u.Password.Equals(password)).ToList();

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

            var result = usersTable.Where(u => u.Username.Equals(login)).ToList();

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

            return usersTable.Contains(new User() { Username = username });
        }
        public void AddUserSafely(String username, String password, String firstName, String lastName)
        {
            int id = usersTable.Count;
            User user = new User() {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Password = password 
            };

            usersTable.Add(user);
        }
        public void AddUserSafely(User user)
        {
            if (user == null)
                throw new ArgumentNullException();

            AddUserSafely(user.Username, user.Password, user.FirstName, user.LastName);
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

            trans.IsPermited = false;
            transactionsTable.Add(trans);
        }
        public List<Transaction> GetTransactionsForUser(int userId)
        {
            /*
             Предполагается, что user1Id инициатор, а user2Id тот, на кого заводят транзакцию
             */
            var query = transactionsTable.Where(t => t.IsPermited == false && t.User2Id == userId);
            if (query.Count() == 0)
                return null;

            return query.ToList();
        }
        public bool ConfirmTransaction(Transaction trans)
        {
            if (trans == null)
                throw new ArgumentNullException();


            //transaction that belongs to user
            var query = transactionsTable.Where(t => t.Id == trans.Id && 
            t.User1Id == trans.User1Id && t.User2Id == trans.User2Id);

            if (query.Count() == 0)
                return false;

            Transaction localTrans = query.First();
            
            localTrans.IsPermited = true;

            return true;
        }
        public bool CloseTransactionForUser(int transId, int userId)
        {
            //todo: implement IEquatable<Transaction> for more flexibility
            var query = transactionsTable.Where(t => t.Id == transId && t.User1Id == userId);

            int count = query.Count();
            if (count == 0 || count > 1)
                return false;

            Transaction localTrans = query.First();
            localTrans.IsClosed = true;
            
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
            var query = transactionsTable.Where(t => t.IsPermited == false && t.User1Id == userId);
            if (query.Count() == 0)
                return null;

            return query.ToList();
        }

        bool IDataAccess.SetTokenForUser(int userId, string token)
        {
            throw new NotImplementedException();
        }

        public bool ChangeUserInfo(User user)
        {
            throw new NotImplementedException();
        }
    }
}
