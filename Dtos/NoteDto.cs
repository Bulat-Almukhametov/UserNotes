using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserNotes.Dtos
{
    public class NoteDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public UserDto User { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
