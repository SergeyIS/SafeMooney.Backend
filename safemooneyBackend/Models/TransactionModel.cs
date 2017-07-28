using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace safemooneyBackend.Models
{
    public class TransactionModel
    {
        public int userId { get; set; }
        public string count { get; set; }
        public DateTime date { get; set; }
        public int period { get; set; }
    }
}