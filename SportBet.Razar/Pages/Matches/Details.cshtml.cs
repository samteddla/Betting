using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportBet.Razar.Model;

namespace SportBet.Razar.Pages.Matches
{
    public class DetailsModel : PageModel
    {
        private readonly SportBet.Razar.Model.BetOnContext _context;

        public DetailsModel(SportBet.Razar.Model.BetOnContext context)
        {
            _context = context;
        }

        public Match Match { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches.FirstOrDefaultAsync(m => m.MatchId == id);
            if (match == null)
            {
                return NotFound();
            }
            else
            {
                Match = match;
            }
            return Page();
        }
    }
}
