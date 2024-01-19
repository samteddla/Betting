using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportBet.Razar.Model;

namespace SportBet.Razar.Pages.BetCards
{
    public class DeleteModel : PageModel
    {
        private readonly SportBet.Razar.Model.BetOnContext _context;

        public DeleteModel(SportBet.Razar.Model.BetOnContext context)
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

            var betcard = await _context.BetCards.FirstOrDefaultAsync(m => m.BetCardId == id);

            if (betcard == null)
            {
                return NotFound();
            }
            else
            {
                BetCard = betcard;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var betcard = await _context.BetCards.FindAsync(id);
            if (betcard != null)
            {
                BetCard = betcard;
                _context.BetCards.Remove(BetCard);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
