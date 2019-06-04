using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserNotes.Dtos;

namespace UserNotes.Controllers
{
    [Authorize]
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
        public PaginatedDataDto<NoteDto> AllPublic(int page, int size)
        {
            int skipItems = (page - 1) * size;

            int totalItems = 50;
            int notesCount = (totalItems - skipItems) > size ? size : (totalItems - skipItems);
            var notes = new NoteDto[notesCount];

            for (int i = 0, j = skipItems; i < notesCount; i++, j++)
            {
                var user = GetRandomUser();
                notes[i] = new NoteDto
                {
                    User = user,
                    Title = $"Comment {j + 1}",
                    Body = $"{ user.Nickname } says some interestin thing: \"So, I think bla bla bla!\"",
                    CreateTime = DateTime.Now
                };
            }

            return new PaginatedDataDto<NoteDto>
            {
                Data = notes,
                TotalItems = totalItems
            };
        }
    }
}
