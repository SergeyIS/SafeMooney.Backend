using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharedResourcesLibrary;
using SharedResourcesLibrary.Models;

namespace safemooneyBackend.Models
{
    public class TransactionResponseModel
    {
        public Transaction transactionData { get; set; } 
        public UserResponseModel userData { get; set; }       
    }
}