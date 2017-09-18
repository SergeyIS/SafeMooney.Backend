using SharedResourcesLibrary;

namespace safemooneyBackend.Models
{
    public class TransactionResponseModel
    {
        public Transaction transactionData { get; set; } 
        public UserResponseModel userData { get; set; }       
    }
}