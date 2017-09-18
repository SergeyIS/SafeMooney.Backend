using System;

namespace safemooneyBackend.Models
{
    public class TransactionRequestModel
    {
        public int transactionId { get; set; }
        public int userId { get; set; }
        public string count { get; set; }
        public DateTime date { get; set; }
        public int period { get; set; }
        public String comment { get; set; }
    }
}