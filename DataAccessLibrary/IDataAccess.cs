using System;
using System.Collections.Generic;
using SharedResourcesLibrary;

namespace DataAccessLibrary
{
    public interface IDataAccess
    {
        User FindUser(String login, String password);
        User FindUserById(int id);
        User FindUserByLogin(String login);
        User FindUserByToken(String TokenKey);

        AuthService GetServiceData(int providerId, String authId);
        bool AddServiceData(AuthService service);
        bool ChangeServiceData(AuthService service);

        bool CheckForUser(String username);
        void AddUserSafely(String username, String password, String firstName, String lastName);
        void AddUserSafely(User user);
        List<User> GetAllUsers();
        void AddTransaction(Transaction trans);
        List<Transaction> GetTransactionsForUser(int userID);
        bool ConfirmTransaction(int transId, int userId);
        bool ResetTokenForUser(int userId);
        bool CloseTransactionForUser(int transId, int userId);
        bool RemoveUser(int userId, ref String token);
        List<Transaction> FetchTransactions(int userId);
        bool SetTokenForUser(int userId, String token);
        bool ChangeUserInfo(User user);
        bool SetImage(int userId, byte[] bytes);
        byte[] GetImage(int userId);
        List<User> GetUsers(String fname, String lname);
        List<User> GetUsers(String username);
    }
}
