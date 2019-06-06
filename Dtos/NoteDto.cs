using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserNotes.Dtos
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public UserDto User { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsPublic { get; set; }
    }
}
