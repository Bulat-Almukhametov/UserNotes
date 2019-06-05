using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserNotes.Models
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("body")]
        public string Body { get; set; }
        [Column("public")]
        public bool IsPublic { get; set; }
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
    }
}
