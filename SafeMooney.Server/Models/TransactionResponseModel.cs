using System;
using SafeMooney.Shared.Models;

namespace SafeMooney.Server.Models
{
    public class TransactionResponseModel
    {
        public Transaction transactionData { get; set; } 
        public UserResponseModel userData { get; set; }
    }
}
