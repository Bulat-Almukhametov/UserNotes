using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserNotes.Dtos;
using UserNotes.Infrastructure;
using UserNotes.Models;

namespace UserNotes.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : Controller
    {
        private UserNotesDbContext _Context;
        private int _CurrentUserId;
        private bool _AdminMode;
        public NoteController(UserNotesDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _Context = context;
            var user = httpContextAccessor?.HttpContext?.User;
            if (user != null)
            {
                var userIdString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var isAdminString = user.FindFirst(ClaimTypes.Role)?.Value;

                if (userIdString != null)
                    _CurrentUserId = Convert.ToInt32(userIdString);
                _AdminMode = isAdminString == "admin";
            }
        }

        [HttpGet("[action]")]
        public PaginatedDataDto<NoteDto> All(int page, int size, bool isPublic)
        {
            int skipItems = (page - 1) * size;

            IQueryable<Note> query = _Context.Notes.Include(n => n.User);
            if (_AdminMode)
            {
                query = query.Where(n => n.IsPublic == isPublic);
            }
            else
            {
                query = query.Where(n => n.IsPublic);
            }

            var notesPage = query.OrderBy(n => n.CreateTime)
                .Skip(skipItems)
                .Take(size)
                .GroupBy(n => new { Total = query.Count() })
                .First();

            return new PaginatedDataDto<NoteDto>
            {
                Data = notesPage.Select(n => new NoteDto
                {
                    Id = n.Id,
                    User = new UserDto { Nickname = n.User.Login, Admin = n.User.IsAdmin },
                    Title = n.Title,
                    Body = n.Body,
                    CreateTime = n.CreateTime,
                    IsPublic = n.IsPublic
                }),
                TotalItems = notesPage.Key.Total
            };
        }

        [HttpPost("[action]")]
        public async Task Edit(NoteDto input)
        {
            var entity = new Note
            {
                Id = input.Id,
                UserId = _CurrentUserId,
                Title = input.Title,
                Body = input.Body,
                CreateTime = input.CreateTime == default(DateTime) ? DateTime.Now : input.CreateTime,
                IsPublic = input.IsPublic
            };
            if (entity.Id != 0)
            {
                _Context.Notes.Attach(entity);
                _Context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                await _Context.Notes.AddAsync(entity);
            }

            await _Context.SaveChangesAsync();
        }
    }
}
