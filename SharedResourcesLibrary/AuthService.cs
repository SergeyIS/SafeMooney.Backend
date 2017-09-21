using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedResourcesLibrary
{
    public class AuthService
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Key]
        [Column("provider_id")]
        public int ProviderId { get; set; }

        [Column("auth_id")]
        public String AuthId { get; set; }

        [Column("auth_token")]
        public String AuthToken { get; set; }

        [Column("auth_param")]
        public String AuthParam { get; set; }
    }
}
