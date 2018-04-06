using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SafeMooney.Shared;
using SafeMooney.Shared.Models;
using SafeMooney.Server.Models;
using SafeMooney.Server.Security.Filters;
using SafeMooney.Server.Infrastructure.Dependencies;
using NLog;

namespace SafeMooney.Server.Controllers
{
    [AuthFilter]
    public class TransactionsController : ApiController
    {
        private Logger _logger = null;
        private IDataStorage _db = null;

        public TransactionsController()
        {
            _logger = LogManager.GetCurrentClassLogger();

            try
            {
                _db = (IDataStorage)DependencyContainer.GetService(typeof(IDataStorage));
            }
            catch
            {
                _logger.Error("Cannot get database instance from IoC container");
            }
        }

        /// <summary>
        /// This method reterns users who are friends of user with userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/{userId}/transactions/getuserlist")]
        public HttpResponseMessage GetUserList(int userId)
        {
            if (_db == null)
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            if (userId < 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            //todo: retern users who are friends of user with userId
            //There is opportunity to create transaction for yourself
            IEnumerable<ShortUserModel> userList = null;
            try
            {
                userList = _db.GetAllUsers().Select(u => new ShortUserModel()
                {
                    UserId = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Username = u.Username
                });
            }
            catch(Exception e)
            {
                //write log
                _logger.Error("Cannot get all users", e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

            //write log
            _logger.Info($"user {userId} request userlist|response count: {userList.Count()}");

            return Request.CreateResponse(HttpStatusCode.OK, userList);
        }

        [HttpGet]
        [Route("api/{userId}/transactions/getuserlist")]
        public HttpResponseMessage GetUserList(int userId, [FromUri]String search)
        {
            if (_db == null)
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            //todo: Implement logic of working with search parametr. It's necessary to get fname, lname or _username from search
            if (search == null || String.IsNullOrEmpty(search) || search.Length > 50)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                String[] values = search.Split(' ');
                List<ShortUserModel> userList = new List<ShortUserModel>();

                if (values.Length == 1)
                {
                    var query = _db.GetUsers(values[0]).Select(u => new ShortUserModel()
                    {
                        UserId = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Username = u.Username
                    });

                    if (query != null)
                        userList = query.ToList();
                }
                else if (values.Length == 2)
                {
                    var firstQuery = _db.GetUsers(values[0], values[1]).Select(u => new ShortUserModel()
                    {
                        UserId = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Username = u.Username
                    });

                    var secondQuery = _db.GetUsers(values[1], values[0]).Select(u => new ShortUserModel()
                    {
                        UserId = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Username = u.Username
                    });

                    if (firstQuery != null)
                        userList.AddRange(firstQuery.ToList());

                    if (secondQuery != null)
                        userList.AddRange(secondQuery.ToList());

                }

                //write log
                _logger.Info($"user {userId} request userlist|response count: {userList.Count()}");

                return Request.CreateResponse(HttpStatusCode.OK, userList);
            }
            catch(Exception e)
            {
                _logger.Error("An error was occured while getting users proccess", e);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }            
        }

        /// <summary>
        /// This method add new not permited transaction
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/{userId}/transactions/add")]
        public HttpResponseMessage Add([FromBody]TransactionRequestModel trans, int userId)
        {
            if (_db == null)
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            if (userId < 0 || trans == null || !trans.IsValid())
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                Transaction transactionObj = new Transaction()
                {
                    User1Id = userId,
                    User2Id = trans.userId,
                    Count = trans.count,
                    Date = DateTime.Parse(trans.date),
                    Period = trans.period,
                    IsPermited = false,
                    IsClosed = false,
                    Comment = trans.comment
                };

                _db.AddTransaction(transactionObj);

                //write log
                _logger.Info($"user {userId} added transaction");

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception e)
            {
                //write log
                _logger.Error($"An error was encountered while adding a transaction|user_id: {userId}");

                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// This method checks transactions table and retern not permited for this user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/{userId}/transactions/checkqueue")]
        public HttpResponseMessage CheckQueue(int userId)
        {
            if (_db == null)
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            if (userId < 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            List<Transaction> transactions = null;

            try
            {
                transactions = _db.GetTransactionsForUser(userId);
            }
            catch(Exception e)
            {
                _logger.Error($"Cannot get transactions for user {userId}", e);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

            if(transactions == null)
                return Request.CreateResponse(HttpStatusCode.OK);

            List<TransactionResponseModel> response = new List<TransactionResponseModel>(transactions.Count);
            foreach (var item in transactions)
            {
                try
                {
                    User user = _db.FindUserById((item.User1Id == userId) ? item.User2Id : item.User1Id);
                    if (user == null)
                        continue;

                    response.Add(new TransactionResponseModel()
                    {
                        transactionData = item,
                        userData = new UserResponseModel()
                        {
                            UserId = user.Id,
                            Username = user.Username,
                            FirstName = user.FirstName,
                            LastName = user.LastName
                        }
                    });
                }
                catch(Exception e)
                {
                    //write log
                    _logger.Error($"An error was encountered while adding a transaction|user_id: {userId}");
                }
                
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Allows to confirm transaction
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/{userId}/transactions/confirm/{transId}")]
        public HttpResponseMessage Confirm(int userId, int transId)
        {
            if (_db == null)
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            if (userId < 0 || transId < 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
          
            try
            {
                bool resultOfOperation = _db.ConfirmTransaction(transId, userId);

                if (!resultOfOperation)
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch(Exception e)
            {
                //write log
                _logger.Error($"An error was encountered while confirming a transaction|user_id: {userId}", e.Message);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

            return Request.CreateResponse(HttpStatusCode.OK);

        }

        /// <summary>
        /// This method close transaction
        /// </summary>
        /// <param name="transId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/{userId}/transactions/close/{transId}")]
        public HttpResponseMessage Close(int transId, int userId)
        {
            if (_db == null)
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            if (userId < 0 || transId < 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            try
            {
                bool resultOfOperation = _db.CloseTransactionForUser(transId, userId);

                if (!resultOfOperation)
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception e)
            {
                //write log
                _logger.Error($"An error was encountered while closing a transaction|user_id: {userId}", e.Message);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// This method retern all contains transactions for user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/{userId}/transactions/fetch")]
        public HttpResponseMessage Fetch(int userId)
        {
            if (_db == null)
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            if (userId < 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            List<Transaction> transactions = null;

            try
            {
                transactions = _db.FetchTransactions(userId);
            }
            catch(Exception e)
            {
                _logger.Error($"Cannot fetch transactions for user {userId}", e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

            if (transactions == null)
                return Request.CreateResponse(HttpStatusCode.OK);

            List<TransactionResponseModel> response = new List<TransactionResponseModel>(transactions.Count);
            foreach (var item in transactions)
            {
                try
                {
                    User user = _db.FindUserById((item.User1Id == userId) ? item.User2Id : item.User1Id);
                    if (user == null)
                        continue;

                    response.Add(new TransactionResponseModel()
                    {
                        transactionData = item,
                        userData = new UserResponseModel()
                        {
                            UserId = user.Id,
                            Username = user.Username,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            PhotoUri = $"http://{Request.RequestUri.Host}:{Request.RequestUri.Port}/api/getimg/{userId}.jpg"
                        }
                    });
                }
                catch(Exception e)
                {
                    //write log
                    _logger.Error($"An error was encountered while adding a transaction|user_id: {userId}");
                }            
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}