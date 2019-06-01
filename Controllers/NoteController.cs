using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserNotes.Dtos;

namespace UserNotes.Controllers
{
    [Route("api/[controller]")]
    public class NoteController : Controller
    {
        private UserDto[] _Users = new[]
            {
                new UserDto { Nickname = "Mickey Mouse", Admin = false },
                new UserDto { Nickname = "Donald Duck", Admin = false },
                new UserDto { Nickname = "Goofy", Admin = false },
                new UserDto { Nickname = "Pluto", Admin = false },
                new UserDto { Nickname = "Walt Disney", Admin = true },
                new UserDto { Nickname = "Scrooge McDuck", Admin = false },
            };

        private UserDto GetRandomUser()
        {
            var rnd = new Random();
            var index = rnd.Next(0, _Users.Length - 1);

            return _Users[index];
        }

        [HttpGet("[action]")]
        public IEnumerable<NoteDto> AllPublic()
        {
            int notesCount = 50;
            var notes = new NoteDto[notesCount];

            for (int i = 0; i < notesCount; i++)
            {
                var user = GetRandomUser();
                notes[i] = new NoteDto
                {
                    User = user,
                    Title = $"Comment {i + 1}",
                    Body = $"{ user.Nickname } says some interestin thing: \"So, I think bla bla bla!\"",
                    CreateTime = DateTime.Now
                };
            }

            return notes;
        }
    }
}
