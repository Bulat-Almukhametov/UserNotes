using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserNotes.Models;

namespace UserNotes.Infrastructure
{
    public class UserNotesDbContext : DbContext
    {
        public UserNotesDbContext(DbContextOptions<UserNotesDbContext> options)
               : base(options)
        { }

        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
