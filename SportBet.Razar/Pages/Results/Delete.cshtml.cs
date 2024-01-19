using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportBet.Razar.Model;

namespace SportBet.Razar.Pages.Results
{
    public class DeleteModel : PageModel
    {
        private readonly SportBet.Razar.Model.BetOnContext _context;

        public DeleteModel(SportBet.Razar.Model.BetOnContext context)
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

            var betresult = await _context.BetResults.FirstOrDefaultAsync(m => m.BetResultId == id);

            if (betresult == null)
            {
                return NotFound();
            }
            else
            {
                BetResult = betresult;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var betresult = await _context.BetResults.FindAsync(id);
            if (betresult != null)
            {
                BetResult = betresult;
                _context.BetResults.Remove(BetResult);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
