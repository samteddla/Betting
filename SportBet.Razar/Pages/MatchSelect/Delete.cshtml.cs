using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportBet.Razar.Model;

namespace SportBet.Razar.Pages.MatchSelect
{
    public class DeleteModel : PageModel
    {
        private readonly SportBet.Razar.Model.BetOnContext _context;

        public DeleteModel(SportBet.Razar.Model.BetOnContext context)
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

            var matchselection = await _context.MatchSelections.FirstOrDefaultAsync(m => m.MatchSelectionId == id);

            if (matchselection == null)
            {
                return NotFound();
            }
            else
            {
                MatchSelection = matchselection;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchselection = await _context.MatchSelections.FindAsync(id);
            if (matchselection != null)
            {
                MatchSelection = matchselection;
                _context.MatchSelections.Remove(MatchSelection);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
