using SharedResourcesLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DataAccessLibrary
{
    public class DataBuilder : IDataAccess
    {
        public User FindUser(String login, String password)
        {
            if (login == null || password == null)
                throw new Exception("It's not allowed to use empty arguments");

            DataStorageContext db = null;
            try
            {
                using (db = new DataStorageContext())
                {
                    var query = db.Users.Where(u => u.Login.Equals(login) && u.Password.Equals(password));

                    if (query.Count() == 0)
                        return null;

                    if (query.Count() > 1)
                        throw new Exception("There's more than one user in the database with such parameters");

                    return query.First();
                }
            }
            catch(Exception e)
            {
                //todo: write log
                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch(Exception ine)
                {
                    //write log
                }

                return null;
            }          
        }
        public User FindUserById(int id)
        {
            DataStorageContext db = null;
            try
            {
                using (db = new DataStorageContext())
                {
                    var query = db.Users.Where(u => u.Id == id);

                    if (query.Count() == 0)
                        return null;

                    if (query.Count() > 1)
                        throw new Exception("There's more than one user in the database with such parameters");

                    return query.First();
                }
            }
            catch(Exception e)
            {
                //todo: write log
                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch(Exception ine)
                {
                    //todo: write log
                }

                return null;
            }
            
        }
        public User FindUserByLogin(String login)
        {
            if (login == null)
                throw new Exception("It's not allowed to use empty argument");

            DataStorageContext db = null;
            try
            {
                using (db = new DataStorageContext())
                {
                    var query = db.Users.Where(u => u.Login.Equals(login));

                    if (query.Count() == 0)
                        return null;

                    if (query.Count() > 1)
                        throw new Exception("There's more than one user in the database with such parameter");

                    return query.First();
                }
            }
            catch(Exception e)
            {
                //todo: write log
                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch(Exception ine)
                {
                    //todo: write log
                }

                return null;
            }
        }
        public User FindUserByToken(String TokenKey)
        {
            throw new NotImplementedException();
        }
        public bool CheckForUser(String username)
        {
            if (username == null)
                throw new ArgumentException();

            DataStorageContext db = null;
            try
            {
                using (db = new DataStorageContext())
                {
                    return db.Users.Contains(new User() { Login = username });
                }
            }
            catch(Exception e)
            {
                //todo: write log
                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch(Exception ine)
                {
                    //todo: write log
                }

                return false;
            }
        }
        public void AddUserSafely(String username, String password, String firstName, String lastName)
        {
            DataStorageContext db = null;
            try
            {
                using (db = new DataStorageContext())
                {
                    int id = db.Users.Count();
                    User user = new User()
                    {
                        Id = id,
                        FirstName = firstName,
                        LastName = lastName,
                        Login = username,
                        Password = password
                    };

                    db.Users.Add(user);
                    db.SaveChangesAsync();
                }
            }
            catch(Exception e)
            {
                //todo: write log
                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch(Exception ine)
                {
                    //todo: write log
                }
            }   
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

            DataStorageContext db = null;
            try
            {
                using (db = new DataStorageContext())
                {
                    db.Users.Add(user);
                }

                return true;
            }
            catch(Exception e)
            {
                //todo: write log
                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch(Exception ine)
                {
                    //todo: write log
                }

                return false;
            }
        }
        public bool RemoveUser(int userId, ref String token)
        {          
            DataStorageContext db = null;
            try
            {
                using(db = new DataStorageContext())
                {
                    var query = db.Users.Where(u => u.Id == userId);

                    if (query.Count() == 0)
                        return false;

                    if (query.Count() > 1)
                        throw new Exception("There's more than one user in the database with such parameters");

                    User user = query.First();

                    user.TokenKey = token;
                    db.Entry<User>(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception e)
            {
                //todo: write log
                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch(Exception ine)
                {
                    //todo: write log
                }

                return false;
            }
        }
        public List<User> GetAllUsers()
        {
            DataStorageContext db = null;
            try
            {
                using(db = new DataStorageContext())
                {
                    return db.Users.ToList();
                }
            }
            catch(Exception e)
            {
                //todo: write log
                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch(Exception ine)
                {
                    //todo: write log
                }

                return null;
            }
        }

        public void AddTransaction(Transaction trans)
        {
            if (trans == null)
                throw new ArgumentNullException();

            DataStorageContext db = null;
            try
            {
                using(db = new DataStorageContext())
                {
                    int index = db.Users.Count();
                    trans.transactionId = index;
                    trans.isPermited = false;
                    db.Entry<Transaction>(trans).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChangesAsync();
                }
            }
            catch(Exception e)
            {
                //todo: write log
                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch(Exception ine)
                {
                    //todo: write log
                }
            }
        }

        public List<Transaction> GetTransactionsForUser(int userId)
        {
            //Предполагается, что user1Id инициатор, а user2Id тот, на кого заводят транзакцию

            DataStorageContext db = null;
            try
            {
                using(db = new DataStorageContext())
                {
                    var query = db.Transactions.Where(t => t.isPermited == false && t.user2Id == userId);
                    if (query.Count() == 0)
                        return null;

                    return query.ToList();
                }
            }
            catch (Exception e)
            {
                //todo: write log
                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch (Exception ine)
                {
                    //todo: write log
                }

                return null;
            }           
        }

        public bool ConfirmTransaction(Transaction trans)
        {
            if (trans == null)
                throw new ArgumentNullException();

            DataStorageContext db = null;
            try
            {
                using(db = new DataStorageContext())
                {
                    var query = db.Transactions.Where(t => t.transactionId == trans.transactionId &&
                        t.user1Id == trans.user1Id && t.user2Id == trans.user2Id);

                    if (query.Count() == 0)
                        return false;

                    Transaction localtrans = query.First();
                    localtrans.isPermited = true;
                    db.Entry<Transaction>(localtrans).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception e)
            {
                //todo: write log
                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch (Exception ine)
                {
                    //todo: write log
                }

                return false;
            }
        }

        public bool CloseTransactionForUser(int transId, int userId)
        {
            DataStorageContext db = null;
            try
            {
                using(db = new DataStorageContext())
                {
                    var query = db.Transactions.Where(t => t.transactionId == transId && t.user1Id == userId);

                    int count = query.Count();
                    if (count == 0 || count > 1)
                        return false;

                    Transaction localTrans = query.First();
                    localTrans.isClosed = true;
                    db.Entry<Transaction>(localTrans).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception e)
            {
                //todo: write log
                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch (Exception ine)
                {
                    //todo: write log
                }

                return false;
            }
        }

        public bool ResetTokenForUser(int userId)
        {
            DataStorageContext db = null;
            try
            {
                using (db = new DataStorageContext())
                {
                    var query = db.Users.Where(u => u.Id == userId);
                    if (query.Count() == 0)
                        return false;

                    if (query.Count() > 1)
                        throw new Exception("More than one user was found");

                    User localUser = query.First();
                    localUser.TokenKey = null;

                    db.Entry<User>(localUser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception e)
            {
                //todo: write log
                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch (Exception ine)
                {
                    //todo: write log
                }

                return false;
            }

        }

        public List<Transaction> FetchTransactions(int userId)
        {
            //Предполагается, что user1Id инициатор, а user2Id тот, на кого заводят транзакцию
            DataStorageContext db = null;
            try
            {
                using (db = new DataStorageContext())
                {
                    var query = db.Transactions.Where(t => t.isPermited == false && t.user1Id == userId);
                    if (query.Count() == 0)
                        return null;

                    return query.ToList();
                }
            }
            catch (Exception e)
            {
                //todo: write log
                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch (Exception ine)
                {
                    //todo: write log
                }

                return null;
            }
            
        }
    }
}
