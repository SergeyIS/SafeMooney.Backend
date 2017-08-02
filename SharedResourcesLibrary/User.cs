using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedResourcesLibrary
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("firstname")]
        public String FirstName { get; set; }

        [Required]
        [Column("lastname")]
        public String LastName { get; set; }

        [Required]
        [Column("username")]
        public String Username { get; set; }

        [Required]
        [Column("password")]
        public String Password { get; set; }

        [Column("tokenkey")]
        public String TokenKey { get; set; }
    }
}
