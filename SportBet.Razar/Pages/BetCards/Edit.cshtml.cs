using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportBet.Razar.Model;

namespace SportBet.Razar.Pages.BetCards
{
    public class EditModel : PageModel
    {
        private readonly SportBet.Razar.Model.BetOnContext _context;

        public EditModel(SportBet.Razar.Model.BetOnContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BetCard BetCard { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var betcard =  await _context.BetCards.FirstOrDefaultAsync(m => m.BetCardId == id);
            if (betcard == null)
            {
                return NotFound();
            }
            BetCard = betcard;
           ViewData["MatchSelectionId"] = new SelectList(_context.MatchSelections, "MatchSelectionId", "MatchSelectionId");
           ViewData["MatchTypeId"] = new SelectList(_context.BetMatchTypes, "MatchTypeId", "MatchTypeId");
           ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "PersonId");
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

            _context.Attach(BetCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BetCardExists(BetCard.BetCardId))
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

        private bool BetCardExists(int id)
        {
            return _context.BetCards.Any(e => e.BetCardId == id);
        }
    }
}
