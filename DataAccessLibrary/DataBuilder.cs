using System;
using System.Collections.Generic;
using System.Linq;
using SharedResourcesLibrary;
using NLog;

namespace DataAccessLibrary
{
    public class DataBuilder : IDataAccess
    {
        Logger logger = null;

        public DataBuilder()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        public User FindUser(String login, String password)
        {
            if (login == null || password == null)
                throw new Exception("It's not allowed to use empty arguments");

            DataStorageContext db = null;
            try
            {
                using (db = new DataStorageContext())
                {
                    var query = db.Users.Where(u => u.Username.Equals(login) && u.Password.Equals(password));

                    if (query.Count() == 0)
                        return null;

                    if (query.Count() > 1)
                        throw new Exception("There's more than one user in the database with such parameters");

                    return query.First();
                }
            }
            catch(Exception e)
            {
                if (logger != null)
                    logger.WarnException(e.Message, e);

                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch(Exception ine)
                {
                    if (logger != null)
                        logger.WarnException(ine.Message, ine);
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
                if (logger != null)
                    logger.WarnException(e.Message, e);

                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch(Exception ine)
                {
                    if (logger != null)
                        logger.WarnException(ine.Message, ine);
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
                    var query = db.Users.Where(u => u.Username.Equals(login));

                    int countOfUsers = query.Count();
                    if (countOfUsers == 0)
                        return null;

                    if (countOfUsers > 1)
                        throw new Exception("There's more than one user in the database with such parameter");

                    return query.First();
                }
            }
            catch(Exception e)
            {
                if (logger != null)
                    logger.WarnException(e.Message, e);

                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch(Exception ine)
                {
                    if (logger != null)
                        logger.WarnException(ine.Message, ine);
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
                    var query = db.Users.Where(u => u.Username == username);

                    int countOfUsers = query.Count();
                    if (countOfUsers == 0)
                        return false;

                    if (countOfUsers > 1)
                        throw new Exception("More than one user was found");

                    return true;
                }
            }
            catch(Exception e)
            {
                if (logger != null)
                    logger.WarnException(e.Message, e);

                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch(Exception ine)
                {
                    if (logger != null)
                        logger.WarnException(ine.Message, ine);
                }

                throw e;
            }
        }

        public void AddUserSafely(String username, String password, String firstName, String lastName)
        {
            DataStorageContext db = null;
            try
            {
                using (db = new DataStorageContext())
                {
                    User user = new User()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Username = username,
                        Password = password,
                        TokenKey = null
                    };

                    db.Users.Add(user);
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {
                if (logger != null)
                    logger.WarnException(e.Message, e);

                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch(Exception ine)
                {
                    if (logger != null)
                        logger.WarnException(ine.Message, ine);
                }

                throw e;
            }   
        }

        public void AddUserSafely(User user)
        {
            if (user == null)
                throw new ArgumentNullException();

            AddUserSafely(user.Username, user.Password, user.FirstName, user.LastName);
        }

        public bool RemoveUser(int userId, ref String token)
        {          
            DataStorageContext db = null;
            try
            {
                using(db = new DataStorageContext())
                {
                    var query = db.Users.Where(u => u.Id == userId);

                    int countOfUsers = query.Count();
                    if (countOfUsers == 0)
                        return false;

                    if (countOfUsers > 1)
                        throw new Exception("There's more than one user in the database with such parameters");

                    User user = query.First();

                    token = user.TokenKey;
                    db.Entry<User>(user).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                if (logger != null)
                    logger.WarnException(e.Message, e);

                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch(Exception ine)
                {
                    if (logger != null)
                        logger.WarnException(ine.Message, ine);
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
                if (logger != null)
                    logger.WarnException(e.Message, e);

                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch(Exception ine)
                {
                    if (logger != null)
                        logger.WarnException(ine.Message, ine);
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
                    trans.IsPermited = false;
                    db.Entry<Transaction>(trans).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {
                if (logger != null)
                    logger.WarnException(e.Message, e);

                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch(Exception ine)
                {
                    if (logger != null)
                        logger.WarnException(ine.Message, ine);
                }

                throw e;
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
                    var query = db.Transactions.Where(t => t.IsPermited == false && t.User2Id == userId);
                    if (query.Count() == 0)
                        return null;

                    return query.ToList();
                }
            }
            catch (Exception e)
            {
                if (logger != null)
                    logger.WarnException(e.Message, e);

                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch (Exception ine)
                {
                    if (logger != null)
                        logger.WarnException(ine.Message, ine);
                }

                return null;
            }           
        }

        public bool ConfirmTransaction(int transId, int userId)
        {
            if (transId < 0 || userId < 0)
                throw new ArgumentNullException();

            DataStorageContext db = null;
            try
            {
                using(db = new DataStorageContext())
                {
                    var query = db.Transactions.Where(t => t.Id == transId && t.User2Id == userId);

                    if (query.Count() == 0)
                        return false;

                    if (query.Count() > 1)
                        throw new Exception("There're more than one transaction with this id in the database");

                    Transaction trans = query.First();
                    trans.IsPermited = true;
                    db.Entry(trans).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception e)
            {
                if (logger != null)
                    logger.WarnException(e.Message, e);

                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch (Exception ine)
                {
                    if (logger != null)
                        logger.WarnException(ine.Message, ine);
                }

                return false;
            }
        }

        public bool CloseTransactionForUser(int transId, int userId)
        {
            if (transId < 0 || userId < 0)
                throw new ArgumentException("One of arguments has negative value");

            DataStorageContext db = null;
            try
            {
                using(db = new DataStorageContext())
                {
                    var query = db.Transactions.Where(t => t.Id == transId && t.User1Id == userId);

                    int count = query.Count();

                    if (count == 0)
                        return false;

                    if (count > 1)
                        throw new Exception("There're more than one transaction with this id in the database");

                    Transaction localTrans = query.First();
                    localTrans.IsClosed = true;
                    db.Entry(localTrans).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception e)
            {
                if (logger != null)
                    logger.WarnException(e.Message, e);

                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch (Exception ine)
                {
                    if (logger != null)
                        logger.WarnException(ine.Message, ine);
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
                    int countOfUsers = query.Count();
                    if (countOfUsers == 0)
                        return false;

                    if (countOfUsers > 1)
                        throw new Exception("More than one user was found");

                    User localUser = query.First();
                    localUser.TokenKey = null;

                    db.Entry<User>(localUser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception e)
            {
                if (logger != null)
                    logger.WarnException(e.Message, e);

                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch (Exception ine)
                {
                    if (logger != null)
                        logger.WarnException(ine.Message, ine);
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
                    var query = db.Transactions.Where(t => t.IsClosed == false && (t.User1Id == userId || t.User2Id == userId));
                    if (query.Count() == 0)
                        return null;

                    return query.ToList();
                }
            }
            catch (Exception e)
            {
                if (logger != null)
                    logger.WarnException(e.Message, e);

                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch (Exception ine)
                {
                    if (logger != null)
                        logger.WarnException(ine.Message, ine);
                }

                return null;
            }
            
        }

        public bool SetTokenForUser(int userId, String token)
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
                    localUser.TokenKey = token;

                    db.Entry<User>(localUser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception e)
            {
                if (logger != null)
                    logger.WarnException(e.Message, e);

                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch (Exception ine)
                {
                    if (logger != null)
                        logger.WarnException(ine.Message, ine);
                }

                return false;
            }
        }

        public bool ChangeUserInfo(User user)
        {
            DataStorageContext db = null;
            try
            {
                using (db = new DataStorageContext())
                {
                    db.Entry<User>(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                if (logger != null)
                    logger.WarnException(e.Message, e);

                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch (Exception ine)
                {
                    if (logger != null)
                        logger.WarnException(ine.Message, ine);
                }

                return false;
            }
        }


        public bool SetImage(int userId, byte[] bytes)
        {
            DataStorageContext db = null;
            try
            {
                using (db = new DataStorageContext())
                {
                    db.UserImages.Add(new UserImage()
                    {
                        UserId = userId,
                        FileName = userId.ToString() + ".jpg",
                        Data = bytes 
                    });

                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                if (logger != null)
                    logger.WarnException(e.Message, e);

                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch (Exception ine)
                {
                    if (logger != null)
                        logger.WarnException(ine.Message, ine);
                }

                return false;
            }
        }

        public byte[] GetImage(int userId)
        {
            DataStorageContext db = null;
            try
            {
                using (db = new DataStorageContext())
                {
                    var query = db.UserImages.Where(img => img.UserId == userId);
                    if (query == null)
                        return null;

                    return query.First().Data;
                }
            }
            catch (Exception e)
            {
                if (logger != null)
                    logger.WarnException(e.Message, e);

                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch (Exception ine)
                {
                    if (logger != null)
                        logger.WarnException(ine.Message, ine);
                }

                return null;
            }
        }

        /// <summary>
        /// Return userlist. fname or lname parametrs can be NULL when you want search for only fname or lname.
        /// </summary>
        /// <param name="fname">first name</param>
        /// <param name="lname">last name</param>
        /// <returns>userlist</returns>
        public List<User> GetUsers(String fname, String lname)
        {
            if (fname == null && lname == null)
                throw new ArgumentNullException("Both of arguments have NULL value");

            DataStorageContext db = null;
            try
            {
                using (db = new DataStorageContext())
                {
                    IQueryable<User> query = null;
                    if(fname == null)
                    {
                        query = db.Users.Where(u =>u.LastName.Equals(lname, StringComparison.OrdinalIgnoreCase));
                    }
                    else if(lname == null)
                    {
                        query = db.Users.Where(u => u.FirstName.Equals(fname, StringComparison.OrdinalIgnoreCase));
                    }
                    else
                    {
                        query = db.Users.Where(u => u.FirstName.Equals(fname, StringComparison.OrdinalIgnoreCase) && u.LastName.Equals(lname, StringComparison.OrdinalIgnoreCase));
                    }

                    if (query == null)
                        return null;

                    return query.ToList();
                }
            }
            catch (Exception e)
            {
                if (logger != null)
                    logger.WarnException(e.Message, e);

                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch (Exception ine)
                {
                    if (logger != null)
                        logger.WarnException(ine.Message, ine);
                }

                return null;
            }
        }

        public List<User> GetUsers(String username)
        {
            DataStorageContext db = null;
            try
            {
                using (db = new DataStorageContext())
                {
                    var query = db.Users.Where(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
                    if (query == null)
                        return null;

                    return query.ToList();
                }
            }
            catch (Exception e)
            {
                if (logger != null)
                    logger.WarnException(e.Message, e);

                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch (Exception ine)
                {
                    if (logger != null)
                        logger.WarnException(ine.Message, ine);
                }

                return null;
            }
        }

        public AuthService GetServiceData(int providerId, String authId)
        {
            if (providerId < 0 || String.IsNullOrEmpty(authId))
                throw new Exception("It's not allowed to use NULL or empty arguments");

            DataStorageContext db = null;
            try
            {
                using (db = new DataStorageContext())
                {
                    var query = db.AuthServices.Where(v=>v.ProviderId.Equals(providerId) && v.AuthId.Equals(authId));

                    if (query.Count() == 0)
                        return null;

                    if (query.Count() > 1)
                        throw new Exception("There's more than one authorization servises in the database with such parameters");

                    return query.First();
                }
            }
            catch (Exception e)
            {
                if (logger != null)
                    logger.WarnException(e.Message, e);

                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch (Exception ine)
                {
                    if (logger != null)
                        logger.WarnException(ine.Message, ine);
                }

                return null;
            }
        }

        public bool AddServiceData(AuthService service)
        {
            if (service == null)
                throw new ArgumentNullException("service has NULL value");

            DataStorageContext db = null;
            try
            {
                using (db = new DataStorageContext())
                {
                    db.AuthServices.Add(service);
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                if (logger != null)
                    logger.WarnException(e.Message, e);

                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch (Exception ine)
                {
                    if (logger != null)
                        logger.WarnException(ine.Message, ine);
                }

                return false;
            }
        }

        public bool ChangeServiceData(AuthService service)
        {
            if (service == null)
                throw new ArgumentNullException("service has NULL value");

            DataStorageContext db = null;
            try
            {
                using (db = new DataStorageContext())
                {
                    db.Entry<AuthService>(service).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                if (logger != null)
                    logger.WarnException(e.Message, e);

                try
                {
                    if (db != null)
                        db.Database.Connection.Close();
                }
                catch (Exception ine)
                {
                    if (logger != null)
                        logger.WarnException(ine.Message, ine);
                }

                return false;
            }
        }
    }
}
