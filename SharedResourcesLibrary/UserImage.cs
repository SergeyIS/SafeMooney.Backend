using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResourcesLibrary
{
    public class UserImage
    {
        [Key]
        [Column("userId")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Required]
        [Column("filename")]
        public String FileName { get; set; }

        [Column("data")]
        public byte[] Data { get; set; }
    }
}
