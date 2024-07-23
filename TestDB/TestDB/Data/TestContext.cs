using Microsoft.EntityFrameworkCore;
using TestDB.Models;
namespace TestDB.Data
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options) : base(options) { }

        public DbSet<Note> Notes { get; set; } = null!;
    }
}
