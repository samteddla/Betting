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
    public class IndexModel : PageModel
    {
        private readonly SportBet.Razar.Model.BetOnContext _context;

        public IndexModel(SportBet.Razar.Model.BetOnContext context)
        {
            _context = context;
        }

        public IList<BetCard> BetCard { get;set; } = default!;

        public async Task OnGetAsync()
        {
            BetCard = await _context.BetCards
                .Include(b => b.MatchSelection)
                .Include(b => b.MatchType)
                .Include(b => b.Person).ToListAsync();
        }
    }
}
