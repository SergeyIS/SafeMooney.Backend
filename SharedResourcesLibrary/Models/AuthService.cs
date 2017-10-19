using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedResourcesLibrary.Models
{
    public class AuthService
    {
        [Column(name:"user_id", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(name:"provider_id", Order = 1)]
        public int ProviderId { get; set; }

        [Column("auth_id")]
        public String AuthId { get; set; }

        [Column("auth_token")]
        public String AuthToken { get; set; }

        [Column("auth_param")]
        public String AuthParam { get; set; }
    }
}
