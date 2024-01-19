using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportBet.Razar.Model;

namespace SportBet.Razar.Pages.MatchSelect
{
    public class EditModel : PageModel
    {
        private readonly SportBet.Razar.Model.BetOnContext _context;

        public EditModel(SportBet.Razar.Model.BetOnContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MatchSelection MatchSelection { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchselection =  await _context.MatchSelections.FirstOrDefaultAsync(m => m.MatchSelectionId == id);
            if (matchselection == null)
            {
                return NotFound();
            }
            MatchSelection = matchselection;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MatchSelection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchSelectionExists(MatchSelection.MatchSelectionId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MatchSelectionExists(int id)
        {
            return _context.MatchSelections.Any(e => e.MatchSelectionId == id);
        }
    }
}
