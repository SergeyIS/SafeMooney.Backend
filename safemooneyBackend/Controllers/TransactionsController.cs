using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using safemooneyBackend.Models;
using safemooneyBackend.Security.Filters;
using SharedResourcesLibrary.TransactionResources;
using DataAccessLibrary;

namespace safemooneyBackend.Controllers
{
    [AuthFilter]
    public class TransactionsController : ApiController
    {
        IDataAccess db = new DataStorageEmulator();

        /// <summary>
        /// This method reterns users who are friends of user with userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/{userId}/transactions/getuserlist")]
        public HttpResponseMessage GetUserList(int userId = -1)
        {
            if (userId < 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            //todo: retern users who are friends of user with userId

            IEnumerable<ShortUserModel> userList = db.GetAllUsers().Select(u => new ShortUserModel()
            {
                UserId = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Username = u.Login
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
        public HttpResponseMessage Add([FromBody]TransactionModel trans, int userId = -1)
        {
 
            if(userId < 0 || trans == null || trans.count == null || trans.date == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);


            Transaction transactionObj = new Transaction()
            {
                user1Id = userId,
                user2Id = trans.userId,
                count = trans.count,
                date = trans.date,
                period = trans.period,
                isPermited = false
            };

            db.AddTransaction(transactionObj);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// This method checks transactions table and retern not permited for this user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/{userId}/transactions/checkqueue")]
        public HttpResponseMessage CheckQueue(int userID = -1)
        {
            if (userID < 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            List<Transaction> transactions = db.GetTransactionsForUser(userID);

            if (transactions.Count == 0)
                return Request.CreateResponse(HttpStatusCode.OK);

            return Request.CreateResponse(HttpStatusCode.OK, transactions);
        }

        /// <summary>
        /// Allows to confirm transaction
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/{userId}/transactions/confirm")]
        public HttpResponseMessage Confirm([FromBody]TransactionModel trans, int userId = -1)
        {
            if (userId < 0 || trans == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            Transaction localTrans = new Transaction()
            {
                transactionId = trans.transactionId,
                user1Id = trans.userId,
                user2Id = userId
            };

            bool resultOfOperation = db.ConfirmTransaction(localTrans);

            if (!resultOfOperation)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            return Request.CreateResponse(HttpStatusCode.OK);

        }

    }
}