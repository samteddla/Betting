using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportBet.Razar.Model;

namespace SportBet.Razar.Pages.Results
{
    public class EditModel : PageModel
    {
        private readonly SportBet.Razar.Model.BetOnContext _context;

        public EditModel(SportBet.Razar.Model.BetOnContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BetResult BetResult { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var betresult =  await _context.BetResults.FirstOrDefaultAsync(m => m.BetResultId == id);
            if (betresult == null)
            {
                return NotFound();
            }
            BetResult = betresult;
            ViewData["MatchId"] = new SelectList(_context.Matches, "MatchId", "MatchId");
            ViewData["MatchSelectionId"] = new SelectList(_context.MatchSelections, "MatchSelectionId", "Name");
            ViewData["MatchTypeId"] = new SelectList(_context.BetMatchTypes, "MatchTypeId", "Name");
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

            _context.Attach(BetResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BetResultExists(BetResult.BetResultId))
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

        private bool BetResultExists(int id)
        {
            return _context.BetResults.Any(e => e.BetResultId == id);
        }
    }
}
