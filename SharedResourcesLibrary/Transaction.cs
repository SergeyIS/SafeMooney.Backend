using System;

namespace SharedResourcesLibrary
{
    public class Transaction
    {
        public int transactionId { get; set; }
        public int user1Id { get; set; }
        public int user2Id { get; set; }
        public string count { get; set; }
        public DateTime date { get; set; }
        public int period { get; set; }
        public bool isPermited { get; set; }
        public bool isClosed { get; set; }
    }
}