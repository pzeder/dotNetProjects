using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestDB.Data;
using TestDB.Models;

namespace TestDB.Pages_Notes
{
    public class IndexModel : PageModel
    {
        private readonly TestDB.Data.TestContext _context;

        public IndexModel(TestDB.Data.TestContext context)
        {
            _context = context;
        }

        public IList<Note> Note { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Note = await _context.Notes.ToListAsync();
        }
    }
}
