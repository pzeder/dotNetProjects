using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebNotes.Data;
using WebNotes.Models;

namespace WebNotes.Pages_Notes
{
    public class IndexModel : PageModel
    {
        private readonly WebNotes.Data.WebNotesDBContext _context;

        public IndexModel(WebNotes.Data.WebNotesDBContext context)
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
