using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestDB.Models;

    public class TextContext : DbContext
    {
        public TextContext (DbContextOptions<TextContext> options)
            : base(options)
        {
        }

        public DbSet<TestDB.Models.Note> Note { get; set; } = default!;
    }
