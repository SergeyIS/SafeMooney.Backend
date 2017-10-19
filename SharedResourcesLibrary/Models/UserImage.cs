using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedResourcesLibrary.Models
{
    public class UserImage
    {
        [Key]
        [Column("userId")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Required]
        [Column("name")]
        public String Name { get; set; }

        [Required]
        [Column("format")]
        public String Format { get; set; }

        [Column("data")]
        public byte[] Data { get; set; }
    }
}
