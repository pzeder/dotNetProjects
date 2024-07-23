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
    public class DeleteModel : PageModel
    {
        private readonly TestDB.Data.TestContext _context;

        public DeleteModel(TestDB.Data.TestContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Notes.FindAsync(id);
            if (note != null)
            {
                Note = note;
                _context.Notes.Remove(Note);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
