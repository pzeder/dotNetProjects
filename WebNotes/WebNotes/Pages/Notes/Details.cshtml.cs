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
    public class DetailsModel : PageModel
    {
        private readonly WebNotes.Data.WebNotesDBContext _context;

        public DetailsModel(WebNotes.Data.WebNotesDBContext context)
        {
            _context = context;
        }

        public Note Note { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Notes.FirstOrDefaultAsync(m => m.Id == id);
            if (note == null)
            {
                return NotFound();
            }
            else
            {
                Note = note;
            }
            return Page();
        }
    }
}
