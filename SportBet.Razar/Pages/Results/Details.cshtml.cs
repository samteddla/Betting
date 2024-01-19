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
    public class DetailsModel : PageModel
    {
        private readonly SportBet.Razar.Model.BetOnContext _context;

        public DetailsModel(SportBet.Razar.Model.BetOnContext context)
        {
            _context = context;
        }

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
    }
}
