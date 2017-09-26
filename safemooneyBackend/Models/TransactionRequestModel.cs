using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace safemooneyBackend.Models
{
    public class TransactionRequestModel
    {
        public int transactionId { get; set; }
        public int userId { get; set; }
        public string count { get; set; }
        public String date { get; set; }
        public int period { get; set; }
    }
}