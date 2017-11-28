using System;

namespace SafeMooney.Server.Models
{
    public class TransactionRequestModel : IValidatedModel
    {
        public int transactionId { get; set; }
        public int userId { get; set; }
        public string count { get; set; }
        public String date { get; set; }
        public int period { get; set; }
        public String comment { get; set; }

        public bool IsValid()
        {
            if (String.IsNullOrEmpty(count) || String.IsNullOrEmpty(date) || period < 0 ||
                date.Length > 20 || comment.Length > 100)
                return false;

            return true;
        }
    }
}
