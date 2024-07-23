using Microsoft.EntityFrameworkCore;
using WebNotes.Models;
namespace WebNotes.Data
{
    public class WebNotesDBContext : DbContext
    {
        public WebNotesDBContext(DbContextOptions<WebNotesDBContext> options) : base(options) { }

        public DbSet<Note> Notes { get; set; } = null!;
    }
}