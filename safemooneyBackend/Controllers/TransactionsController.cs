using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using safemooneyBackend.Models;
using safemooneyBackend.Security.Filters;
using SharedResourcesLibrary;
using DataAccessLibrary;

namespace safemooneyBackend.Controllers
{
    [AuthFilter]
    public class TransactionsController : ApiController
    {
        IDataAccess db = new DataBuilder();

        /// <summary>
        /// This method reterns users who are friends of user with userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/{userId}/transactions/getuserlist")]
        public HttpResponseMessage GetUserList(int userId)
        {
            if (userId < 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            //todo: retern users who are friends of user with userId
            //There is opportunity to create transaction for yourself
            IEnumerable<ShortUserModel> userList = db.GetAllUsers().Select(u => new ShortUserModel()
            {
                UserId = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Username = u.Username
            });
            
            return Request.CreateResponse(HttpStatusCode.OK, userList);
        }

        /// <summary>
        /// This method add new not permited transaction
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/{userId}/transactions/add")]
        public HttpResponseMessage Add([FromBody]TransactionModel trans, int userId)
        {
 
            if(userId < 0 || trans == null || trans.count == null || trans.date == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);


            Transaction transactionObj = new Transaction()
            {
                User1Id = userId,
                User2Id = trans.userId,
                Count = trans.count,
                Date = trans.date,
                Period = trans.period,
                IsPermited = false,
                IsClosed = false
            };

            db.AddTransaction(transactionObj);

            return Request.CreateResponse(HttpStatusCode.OK);
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
            if (userId < 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            List<Transaction> transactions = db.GetTransactionsForUser(userId);

            if (transactions == null)
                return Request.CreateResponse(HttpStatusCode.OK);

            return Request.CreateResponse(HttpStatusCode.OK, transactions);
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
            if (userId < 0 || transId < 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
          
            try
            {
                bool resultOfOperation = db.ConfirmTransaction(transId, userId);

                if (!resultOfOperation)
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch(Exception e)
            {
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
        public HttpResponseMessage Close(int transId = -1, int userId = -1)
        {
            if (userId < 0 || transId < 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            try
            {
                bool resultOfOperation = db.CloseTransactionForUser(transId, userId);

                if (!resultOfOperation)
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception e)
            {
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
        public HttpResponseMessage Fetch(int userId = -1)
        {
            if (userId < 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            return Request.CreateResponse(HttpStatusCode.OK, db.FetchTransactions(userId));
        }
    }
}