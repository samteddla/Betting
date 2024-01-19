using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportBet.Razar.Model;

namespace SportBet.Razar.Pages.Matches
{
    public class EditModel : PageModel
    {
        private readonly SportBet.Razar.Model.BetOnContext _context;

        public EditModel(SportBet.Razar.Model.BetOnContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Match Match { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match =  await _context.Matches.FirstOrDefaultAsync(m => m.MatchId == id);
            if (match == null)
            {
                return NotFound();
            }
            Match = match;
           ViewData["AwayTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId");
           ViewData["HomeTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId");
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

            _context.Attach(Match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(Match.MatchId))
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

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.MatchId == id);
        }
    }
}
