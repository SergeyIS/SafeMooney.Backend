using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeMooney.Shared.Models
{
    public class AuthProvider
    {
        [Key]
        [Column("provider_id")]
        public int ProviderId { get; set; }

        [Column("provider_name")]
        public String ProviderName { get; set; }

    }
}
