using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedResourcesLibrary.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("user1id")]
        public int User1Id { get; set; }

        [Required]
        [Column("user2id")]
        public int User2Id { get; set; }

        [Required]
        [Column("count")]
        public string Count { get; set; }

        [Required]
        [Column("date")]
        public DateTime Date { get; set; }

        [Required]
        [Column("period")]
        public int Period { get; set; }

        [Required]
        [Column("ispermited")]
        public bool IsPermited { get; set; }

        [Required]
        [Column("isclosed")]
        public bool IsClosed { get; set; }

        [Column("comment")]
        public String Comment { get; set; }
    }
}